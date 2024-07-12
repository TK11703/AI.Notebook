using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Models;

public class RequestNewModel
{
	[Required]
	public int Id { get; set; } = 0;

	[Required]
	[Display(Name = "Name")]
	[MinLength(2, ErrorMessage = "The {0} must be at least {1} characters.")]
	[MaxLength(50)]
	public string Name { get; set; } = string.Empty;

	[Required]
	[Display(Name = "AI Resource")]
	public int ResourceId { get; set; }

	public DateTime? CreatedDt { get; set; }

	public DateTime? UpdatedDt { get; set; }
}
