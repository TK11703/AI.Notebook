namespace AI.Notebook.Common.AI.Text;
public class TextTranslationResult
{
	public string? SourceLanguage { get; set; }
	public string? SourceLanguageCode { get; set; }
	public string? TargetLanguage { get; set; }
	public string? TargetLanguageCode { get; set; }
	public string? Input { get; set; }
	public string? Output { get; set; }
	public byte[]? AudioOutput { get; set; }
}
