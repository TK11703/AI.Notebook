using Azure;
using Azure.AI.Translation.Text;
using Microsoft.CognitiveServices.Speech;

namespace AI.Notebook.Common.AI.Text;
public class TextTranslationServices
{
	private readonly TextTranslationClient _translationClient;
	private readonly SpeechConfig _speechConfig;

	public TextTranslationServices(string subscriptionKey, string translationServiceKey, string serviceRegion, string speechServiceKey)
	{
		_translationClient = new TextTranslationClient(new AzureKeyCredential(translationServiceKey), serviceRegion);
		_speechConfig = SpeechConfig.FromSubscription(speechServiceKey, serviceRegion);
	}

	public async Task<SupportedLanguagesResult> GetSupportedLanguages(string? scope = null)
	{
		GetSupportedLanguagesResult response = await _translationClient.GetSupportedLanguagesAsync(scope:scope);
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
		byte[]? audioOutput = new byte[0];
		if (outputAsAudio)
		{
			audioOutput = await GenerateAudio(translation?.Translations?[0]?.Text, voiceName);
		}
		return new TextTranslationResult()
		{
			SourceLanguageCode = translation?.DetectedLanguage?.Language,
			TargetLanguage = translation?.Translations[0].TargetLanguage,
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
		byte[]? audioOutput = new byte[0];
		if (outputAsAudio)
		{
			audioOutput = await GenerateAudio(translation?.Translations?[0]?.Text, voiceName);
		}
		return new TextTranslationResult()
		{
			SourceLanguage = translation?.DetectedLanguage?.Language,
			SourceLanguageCode = sourceLangCode,
			TargetLanguage = translation?.Translations[0].TargetLanguage,
			TargetLanguageCode = targetLangCode,
			Input = input,
			Output = translation?.Translations?[0]?.Text,
			AudioOutput = audioOutput
		};
	}

	private async Task<byte[]?> GenerateAudio(string? textToSpeak, string? voiceName)
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
			using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(_speechConfig, null);
			SpeechSynthesisResult result = await speechSynthesizer.SpeakTextAsync(textToSpeak);
			if (result.Reason == ResultReason.SynthesizingAudioCompleted)
			{
				return result.AudioData;
			}
			if (result.Reason == ResultReason.Canceled)
			{
				var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
				Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

				if (cancellation.Reason == CancellationReason.Error)
				{
					Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
					Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
					Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
				}
			}
		}
		return null;
	}

	public async Task<byte[]?> GenerateSsmlAudio(string ssml)
	{
		if (!string.IsNullOrEmpty(ssml))
		{
			using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(_speechConfig, null);
			SpeechSynthesisResult result = await speechSynthesizer.SpeakSsmlAsync(ssml);
			if (result.Reason == ResultReason.SynthesizingAudioCompleted)
			{
				return result.AudioData;
			}
			if (result.Reason == ResultReason.Canceled)
			{
				var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
				Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

				if (cancellation.Reason == CancellationReason.Error)
				{
					Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
					Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
					Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
				}
			}
		}
		return null;
	}
}
