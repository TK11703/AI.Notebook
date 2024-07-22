using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Models;

public class TranslatorRequestModel : RequestNewModel
{
	[Display(Name = "Source Text")]
	[MinLength(2, ErrorMessage = "The {0} must be at least {1} characters.")]
	public string? Prompt { get; set; } = string.Empty;

	[Required]
	[Display(Name = "Source Language")]	
	public string? SourceLanguage { get; set; } = string.Empty;

	[Display(Name = "Source Script")]
	public string? SourceScript { get; set; } = string.Empty;
	
	[Display(Name = "Target Language")]	
	public string? TargetLanguage { get; set; } = string.Empty;

	[Display(Name = "Target Script")]
	public string? TargetScript { get; set; } = string.Empty;

	[Display(Name = "Translate")]
	public bool Translate { get; set; } = false;

	[Display(Name = "Transliterate")]
	public bool Transliterate { get; set; } = false;

	[Display(Name = "Output as Audio")]
	public bool OutputAsAudio { get; set; } = false;

	[Display(Name = "Voice Name")]
	public string? VoiceName { get; set; } = string.Empty;
}
