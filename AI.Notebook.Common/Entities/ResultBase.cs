
using System.ComponentModel.DataAnnotations.Schema;

namespace AI.Notebook.Common.Entities;

public class ResultBase
{
	public int Id { get; set; }
	public int ResourceId { get; set; }
	public AIResource? AIResource { get; set; }
	public int ResultTypeId { get; set; }
	public ResultType? ResultType { get; set; }
	public int RequestId { get; set; }
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }
	public DateTime? CompletedDt { get; set; }

	[NotMapped]
	public string ItemUrlPath { get; set; } = string.Empty;
}
