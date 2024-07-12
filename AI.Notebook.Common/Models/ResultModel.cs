
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
	public byte[] ResultData { get; set; } = new byte[0];
	public DateTime CompletedDt { get; set; }
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }

	public void CopyBaseData(ResultModel? baseObj)
	{
		if (baseObj != null)
		{
			this.Id = baseObj.Id;
			this.ResourceId = baseObj.ResourceId;
			this.AIResource = baseObj.AIResource;
			this.ResultTypeId = baseObj.ResultTypeId;
			this.ResultType = baseObj.ResultType;
			this.ResultData = baseObj.ResultData;
		}
	}
}
