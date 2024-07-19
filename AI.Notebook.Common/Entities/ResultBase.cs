
using System.ComponentModel.DataAnnotations.Schema;

namespace AI.Notebook.Common.Entities;

public class ResultBase
{
	public int Id { get; set; }
	public int RequestId { get; set; }
	public RequestBase? OriginalRequest { get; set; }
	public int ResourceId { get; set; }
	public AIResource? AIResource { get; set; }
	public int ResultTypeId { get; set; }
	public ResultType? ResultType { get; set; }
	public byte[] ResultData { get; set; } = new byte[0];
	public DateTime CompletedDt { get; set; }
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }

	[NotMapped]
	public string ItemUrlPath { get; set; } = string.Empty;

	public void CopyBaseData(ResultBase? baseObj)
	{
		if (baseObj != null)
		{
			ResourceId = baseObj.ResourceId;
			AIResource = baseObj.AIResource;
			ResultTypeId = baseObj.ResultTypeId;
			ResultType = baseObj.ResultType;
			ResultData = baseObj.ResultData;
		}
	}
}
