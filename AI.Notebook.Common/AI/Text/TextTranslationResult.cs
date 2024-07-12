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

	public byte[] GetResultData()
	{
		if (AudioOutput != null && AudioOutput.Length > 0)
		{
			return AudioOutput;
		}
		return System.Text.Encoding.UTF8.GetBytes(Output ?? string.Empty);
	}
}
