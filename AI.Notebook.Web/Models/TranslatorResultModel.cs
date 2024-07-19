using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Models;

public class TranslatorResultModel : ResultModelBase
{
	public int RequestId { get; set; } = 0;

	[Display(Name = "Source Text")]
	public string? Prompt { get; set; } = string.Empty;

	[Display(Name = "Source Language")]
	public string? SourceLanguage { get; set; } = string.Empty;

	[Display(Name = "Target Language")]
	public string? TargetLanguage { get; set; } = string.Empty;

	[Display(Name = "Translate")]
	public bool Translate { get; set; } = false;

	[Display(Name = "Transliterate")]
	public bool Transliterate { get; set; } = false;

	[Display(Name = "Output as Text")]
	public bool OutputAsText { get { return !OutputAsAudio; } }

	[Display(Name = "Output as Audio")]
	public bool OutputAsAudio { get; set; } = false;

	[Display(Name = "Voice Name")]
	public string? VoiceName { get; set; } = string.Empty;

	[Display(Name = "Translated Text")]
	public string? ResultText { get; set; }

	[Display(Name = "Translated Audio")]
	public byte[]? ResultAudio { get; set; }

	[Display(Name = "Ssml")]
	public string? Ssml { get; set; } = string.Empty;

	[Display(Name = "Ssml Url")]
	public string? SsmlUrl { get; set; } = string.Empty;
}
