using AI.Notebook.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Models;

public class ResultModelBase
{
	public int Id { get; set; } = 0;

	[Display(Name = "AI Resource")]
	public int ResourceId { get; set; }

	public AIResource? AIResource { get; set; }

	[Display(Name = "Result Type")]
	public int ResultTypeId { get; set; }

	public ResultType? ResultType { get; set; }



	[Display(Name = "Date Completed")]
	public DateTime? CompletedDt { get; set; }

	[Display(Name = "Date Created")]
	public DateTime CreatedDt { get; set; }

	[Display(Name = "Date Updated")]
	public DateTime UpdatedDt { get; set; }
}
