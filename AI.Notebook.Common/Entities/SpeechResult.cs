namespace AI.Notebook.Common.Entities;
public class SpeechResult : ResultBase
{
	public string? AudioUrl { get; set; }
	public byte[]? AudioData { get; set; }
	public string? Ssml { get; set; }
	public string? SsmlUrl { get; set; }
	public string? VoiceName { get; set; }
	public string? SourceLangCode { get; set; }
	public string TargetLangCode { get; set; } = string.Empty;
	public bool Transcribe { get; set; } = false;
	public bool Translate { get; set; } = false;
	public bool OutputAsAudio { get; set; } = false;
	public string? ResultText { get; set; }
	public byte[]? ResultAudio { get; set; }
}
