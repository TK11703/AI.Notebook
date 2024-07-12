
using System.ComponentModel.DataAnnotations.Schema;

namespace AI.Notebook.Common.Models;

public class RequestModel
{
	public int Id { get; set; }
	public int ResourceId { get; set; }
	public AIResourceModel? AIResource { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }
	[NotMapped]
	public string ItemUrlPath { get; set; } = string.Empty;

	public void CopyBaseData(RequestModel? baseObj)
	{
		if (baseObj != null)
		{
			this.Id = baseObj.Id;
			this.ResourceId = baseObj.ResourceId;
			this.Name = baseObj.Name;
			this.AIResource = baseObj.AIResource;
		}
	}
}
