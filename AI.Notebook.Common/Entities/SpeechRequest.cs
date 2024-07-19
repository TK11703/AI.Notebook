namespace AI.Notebook.Common.Entities;
public class SpeechRequest : RequestBase
{
	public string? AudioUrl { get; set; }
	public byte[]? AudioData { get; set; }
	public string? VoiceName { get; set; }
	public string? SourceLangCode { get; set; }
	public string TargetLangCode { get; set; } = string.Empty;
	public bool Transcribe { get; set; } = false;
	public bool Translate { get; set; } = false;
	public bool OutputAsAudio { get; set; } = false;
}
