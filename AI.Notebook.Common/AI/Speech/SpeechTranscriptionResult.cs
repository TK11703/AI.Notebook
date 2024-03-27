namespace AI.Notebook.Common.AI.Speech;
public class SpeechTranscriptionResult
{
	public string? SourceLanguage { get; set; }
	public string? SourceLanguageCode { get; set; }
	public string? DetectedLanguage { get; set; }
	public string? TargetLanguageCode { get; set; }
	public string? Output { get; set; }
	public string? ResultId { get; set; }
	public byte[]? AudioOutput { get; set; }
}
