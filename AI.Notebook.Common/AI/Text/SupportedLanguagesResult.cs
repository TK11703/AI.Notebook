
namespace AI.Notebook.Common.AI.Text;

public class SupportedTranslationLanguage
{
	public string Key { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string NativeName { get; set; } = string.Empty;
}

public class SupportedTransliterationLanguage : SupportedTranslationLanguage
{	
	public List<SupportedTransliterationFromScript> Scripts { get; set; } = new List<SupportedTransliterationFromScript>();
}

public class SupportedTransliterationFromScript : SupportedTranslationLanguage
{
	public List<SupportedTransliterationToScript> ToScripts { get; set; } = new List<SupportedTransliterationToScript>();
}

public class SupportedTransliterationToScript
{
	public string Key { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string NativeName { get; set; } = string.Empty;
}

public class SupportedLanguagesResult
{
	public int TranslationLanguages { get; set; } = 0;
	public int TransliterationLanguages { get; set; } = 0;
	public List<SupportedTranslationLanguage> SupportedTranslationLanguages { get; set; }
	public List<SupportedTransliterationLanguage> SupportedTransliterationLanguages { get; set; }

	public SupportedLanguagesResult()
	{
		SupportedTranslationLanguages = new List<SupportedTranslationLanguage>();
		SupportedTransliterationLanguages = new List<SupportedTransliterationLanguage>();
	}
}
