using Azure;
using Azure.AI.Translation.Text;
using Microsoft.CognitiveServices.Speech;

namespace AI.Notebook.Common.AI.Text;
public class TextTranslationServices
{
	private readonly TextTranslationClient _translationClient;
	private readonly SpeechConfig _speechConfig;

	public TextTranslationServices(string serviceKey, string serviceRegion)
	{
		_translationClient = new TextTranslationClient(new AzureKeyCredential(serviceKey), serviceRegion);
		_speechConfig = SpeechConfig.FromSubscription(serviceKey, serviceRegion);
	}

	public async Task<SupportedLanguagesResult> GetSupportedLanguages(string? scope = null)
	{
		GetLanguagesResult response = await _translationClient.GetLanguagesAsync(scope:scope);
		SupportedLanguagesResult result = new SupportedLanguagesResult() {
			TranslationLanguages = response.Translation.Count,
			TransliterationLanguages = response.Transliteration.Count,
			DictionaryLanguages = response.Dictionary.Count
		};
		foreach(var language in response.Translation)
		{
			if(!result.SupportedLanguages.Exists(x=>x.Key == language.Key))
			{
				result.SupportedLanguages.Add(new SupportedLanguage()
				{
					Key = language.Key,
					Name = language.Value.Name,
					SupportsTranslation = true
				});
			}
			else
			{
				result.SupportedLanguages.First(x=>x.Key == language.Key).SupportsTranslation = true;
			}			
		}
		foreach (var language in response.Transliteration)
		{
			if (!result.SupportedLanguages.Exists(x => x.Key == language.Key))
			{
				result.SupportedLanguages.Add(new SupportedLanguage()
				{
					Key = language.Key,
					Name = language.Value.Name,
					SupportsTransliteration = true
				});
			}
			else
			{
				result.SupportedLanguages.First(x => x.Key == language.Key).SupportsTransliteration = true;
			}
		}
		foreach (var language in response.Dictionary)
		{
			if (!result.SupportedLanguages.Exists(x => x.Key == language.Key))
			{
				result.SupportedLanguages.Add(new SupportedLanguage()
				{
					Key = language.Key,
					Name = language.Value.Name,
					SupportsDictionary = true
				});
			}
			else
			{
				result.SupportedLanguages.First(x => x.Key == language.Key).SupportsDictionary = true;
			}
		}
		result.SupportedLanguages.Sort((o1, o2) => o1.Name.CompareTo(o2.Name));
		return result;
	}

	public async Task<TextTranslationResult> Translate(string targetLangCode, string input, bool outputAsAudio = false, string? voiceName = null)
	{
		Response<IReadOnlyList<TranslatedTextItem>> translationResponse = await _translationClient.TranslateAsync(targetLangCode, input);
		IReadOnlyList<TranslatedTextItem> translations = translationResponse.Value;
		TranslatedTextItem translation = translations[0];
		byte[]? audioOutput = null;
		if (outputAsAudio)
		{
			audioOutput = await GenerateAudio(translation?.Translations?[0]?.Text);
		}
		return new TextTranslationResult()
		{
			SourceLanguageCode = translation?.DetectedLanguage?.Language,
			TargetLanguage = translation?.Translations[0].To,
			TargetLanguageCode = targetLangCode,
			Input = input,
			Output = translation?.Translations?[0]?.Text,
			AudioOutput = audioOutput
		};
	}

	public async Task<TextTranslationResult> Translate(string sourceLangCode, string targetLangCode, string input, bool outputAsAudio = false, string? voiceName = null)
	{
		Response<IReadOnlyList<TranslatedTextItem>> translationResponse = await _translationClient.TranslateAsync(targetLanguage: targetLangCode, sourceLanguage: sourceLangCode, text: input);
		IReadOnlyList<TranslatedTextItem> translations = translationResponse.Value;
		TranslatedTextItem translation = translations[0];
		byte[]? audioOutput = null;
		if (outputAsAudio)
		{
			audioOutput = await GenerateAudio(translation?.Translations?[0]?.Text, voiceName);
		}
		return new TextTranslationResult()
		{
			SourceLanguage = translation?.DetectedLanguage?.Language,
			SourceLanguageCode = sourceLangCode,
			TargetLanguage = translation?.Translations[0].To,
			TargetLanguageCode = targetLangCode,
			Input = input,
			Output = translation?.Translations?[0]?.Text,
			AudioOutput = audioOutput
		};
	}

	private async Task<byte[]?> GenerateAudio(string? textToSpeak, string? voiceName = null)
	{
		if (!string.IsNullOrEmpty(textToSpeak))
		{
			if(!string.IsNullOrEmpty(voiceName))
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
		}
		return null;
	}
}
