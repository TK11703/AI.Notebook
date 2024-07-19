namespace AI.Notebook.Common.Entities;
public class TranslatorRequest : RequestBase
{
	public string? SourceLangCode { get; set; }
	public string TargetLangCode { get; set; } = string.Empty;
	public string Input { get; set; } = string.Empty;
	public bool Translate { get; set; } = false;
	public bool Transliterate { get; set; } = false;
	public bool OutputAsAudio { get; set; } = false;
	public string? VoiceName { get; set; }
}
