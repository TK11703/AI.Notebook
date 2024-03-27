
namespace AI.Notebook.Common.Models;

public class ResultModel
{
	public int Id { get; set; }
	public int RequestId { get; set; }
	public RequestModel? OriginalRequest { get; set; }
	public int ResourceId { get; set; }
	public AIResourceModel? AIResource { get; set; }
	public int ResultTypeId { get; set; }
	public ResultTypeModel? ResultType { get; set; }
	public string ResultData { get; set; } = string.Empty;
	public DateTime CompletedDt { get; set; }
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }
}
