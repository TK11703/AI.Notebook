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
			TransliterationLanguages = response.Transliteration.Count
		};
		foreach(var language in response.Translation)
		{
			if(!result.SupportedTranslationLanguages.Exists(x=>x.Key == language.Key))
			{
				result.SupportedTranslationLanguages.Add(new SupportedTranslationLanguage()
				{
					Key = language.Key,
					Name = language.Value.Name,
					NativeName = language.Value.NativeName
				});
			}		
		}
		foreach (var language in response.Transliteration)
		{
			if (!result.SupportedTransliterationLanguages.Exists(x => x.Key == language.Key))
			{
				result.SupportedTransliterationLanguages.Add(BuildTransliterationLanguage(language.Key, language.Value));
			}
		}
		result.SupportedTranslationLanguages.Sort((o1, o2) => o1.Name.CompareTo(o2.Name));
		result.SupportedTransliterationLanguages.Sort((o1, o2) => o1.Name.CompareTo(o2.Name));
		return result;
	}

	private SupportedTransliterationLanguage BuildTransliterationLanguage(string languageKey, TransliterationLanguage lang)
	{
		SupportedTransliterationLanguage newTransliterationLanguage = new SupportedTransliterationLanguage()
		{
			Key = languageKey,
			Name = lang.Name,
			NativeName = lang.NativeName
		};

		foreach (var script in lang.Scripts)
		{
			SupportedTransliterationFromScript newScript = new SupportedTransliterationFromScript()
			{
				Key = script.Code,
				Name = script.Name,
				NativeName = script.NativeName
			};
			foreach (var toScript in script.TargetLanguageScripts)
			{
				newScript.ToScripts.Add(new SupportedTransliterationToScript()
				{
					Key = toScript.Code,
					Name = toScript.Name,
					NativeName = toScript.NativeName
				});
			}
			newTransliterationLanguage.Scripts.Add(newScript);
		}


		return newTransliterationLanguage;
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

	public async Task<TextTransliterationResult> Transliterate(string sourceLangCode, string fromScriptCode, string toScriptCode, IEnumerable<string> input)
	{
		Response<IReadOnlyList<TransliteratedText>> translationResponse = await _translationClient.TransliterateAsync(language: sourceLangCode, toScript: toScriptCode, fromScript: fromScriptCode, content: input);
		IReadOnlyList<TransliteratedText> transliterations = translationResponse.Value;
		TransliteratedText transliteration = transliterations[0];
		return new TextTransliterationResult()
		{			
			SourceLanguageCode = sourceLangCode,
			SourceScriptCode = fromScriptCode,
			TargetScriptCode = toScriptCode,
			Input = string.Join(Environment.NewLine, input),
			Output = transliteration.Text
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
