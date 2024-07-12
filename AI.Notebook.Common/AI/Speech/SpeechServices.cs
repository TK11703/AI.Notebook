using Azure.AI.Translation.Text;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

namespace AI.Notebook.Common.AI.Speech;
public class SpeechServices
{
	private readonly SpeechTranslationConfig _translationConfig;
	private readonly SpeechConfig _speechConfig;

	public SpeechServices(string serviceKey, string serviceRegion)
	{
		_translationConfig = SpeechTranslationConfig.FromSubscription(serviceKey, serviceRegion);
		_speechConfig = SpeechConfig.FromSubscription(serviceKey, serviceRegion);
	}

	public async Task<SpeechTranscriptionResult> Transcribe(string langCode, byte[] audioInput, bool outputAsAudio = false, string? voiceName = null)
	{
		using PushAudioInputStream audioInputStream = AudioInputStream.CreatePushStream();
		audioInputStream.Write(audioInput, audioInput.Length);
		var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(new string[] { "en-US", "de-DE", "zh-CN", langCode });
		using AudioConfig audioConfig = AudioConfig.FromStreamInput(audioInputStream);
		using SpeechRecognizer recognizer = new SpeechRecognizer(_speechConfig, autoDetectSourceLanguageConfig, audioConfig);
		SpeechRecognitionResult result = await recognizer.RecognizeOnceAsync();
		if (result.Reason == ResultReason.RecognizedSpeech)
		{
			var autoDetectLanguage = AutoDetectSourceLanguageResult.FromResult(result);
			byte[]? audioOutput = null;
			if (outputAsAudio)
			{
				audioOutput = await GenerateAudio(result.Text, voiceName);
			}
			return new SpeechTranscriptionResult()
			{
				SourceLanguageCode = langCode,
				DetectedLanguage = autoDetectLanguage.Language,
				ResultId = result.ResultId,
				Output = result.Text,
				AudioOutput = audioOutput
			};
		}
		return new SpeechTranscriptionResult()
		{
			SourceLanguageCode = langCode
		};
	}

	public async Task<SpeechTranslationResult> Translate(string sourceLangCode, string targetLangCode, byte[] audioInput, bool outputAsAudio = false, string? voiceName = null)
	{
		_translationConfig.SpeechRecognitionLanguage = sourceLangCode;
		_translationConfig.AddTargetLanguage(targetLangCode);

		using PushAudioInputStream audioInputStream = AudioInputStream.CreatePushStream();
		audioInputStream.Write(audioInput, audioInput.Length);
		using AudioConfig audioConfig = AudioConfig.FromStreamInput(audioInputStream);
		using TranslationRecognizer recognizer = new TranslationRecognizer(_translationConfig, audioConfig);

		TranslationRecognitionResult result = await recognizer.RecognizeOnceAsync();
		if (result.Reason == ResultReason.TranslatedSpeech)
		{
			byte[]? audioOutput = null;
			if (outputAsAudio)
			{
				audioOutput = await GenerateAudio(result.Translations[targetLangCode], voiceName);
			}
			return new SpeechTranslationResult()
			{
				SourceLanguageCode = sourceLangCode,
				TargetLanguageCode = targetLangCode,
				ResultId = result.ResultId,
				Output = result.Translations[targetLangCode],
				AudioOutput = audioOutput
			};
		}
		return new SpeechTranslationResult()
		{
			SourceLanguageCode = sourceLangCode,
			TargetLanguageCode = targetLangCode
		};
	}
	private async Task<byte[]?> GenerateAudio(string textToSpeak, string? voiceName = null)
	{
		if (!string.IsNullOrEmpty(voiceName))
		{
			_speechConfig.SpeechSynthesisVoiceName = voiceName;
		}
		else
		{
			_speechConfig.SpeechSynthesisVoiceName = "en-GB-RyanNeural";
		}
		using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(_speechConfig);
		SpeechSynthesisResult result = await speechSynthesizer.SpeakTextAsync(textToSpeak);
		if (result.Reason != ResultReason.SynthesizingAudioCompleted)
		{
			return result.AudioData;
		}

		return null;
	}

	private async Task<byte[]?> GenerateSsmlAudio(string responseSsml)
	{
		_speechConfig.SpeechSynthesisVoiceName = string.Empty;
		using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(_speechConfig);
		SpeechSynthesisResult result = await speechSynthesizer.SpeakSsmlAsync(responseSsml);
		if (result.Reason != ResultReason.SynthesizingAudioCompleted)
		{
			return result.AudioData;
		}

		return null;
	}
}
