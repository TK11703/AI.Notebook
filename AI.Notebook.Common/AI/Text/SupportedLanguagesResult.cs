
namespace AI.Notebook.Common.AI.Text;
public class SupportedLanguage
{
	public string Key { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public bool SupportsTranslation { get; set; } = false;
	public bool SupportsTransliteration { get; set; } = false;
	public bool SupportsDictionary { get; set; } = false;
}

public class SupportedLanguagesResult
{
	public int TranslationLanguages { get; set; } = 0;
	public int TransliterationLanguages { get; set; } = 0;
	public int DictionaryLanguages { get; set; } = 0;
	public List<SupportedLanguage> SupportedLanguages { get; set; }

	public SupportedLanguagesResult()
	{
		SupportedLanguages = new List<SupportedLanguage>();
	}
}
