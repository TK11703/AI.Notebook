using System.ComponentModel.DataAnnotations.Schema;

namespace AI.Notebook.Common.Entities;

public class RequestBase
{
	public int Id { get; set; }
	public int ResourceId { get; set; }
	public AIResource? AIResource { get; set; }
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }
	[NotMapped]
	public string ItemUrlPath { get; set; } = string.Empty;

	public void CopyBaseData(RequestBase? baseObj)
	{
		if (baseObj != null)
		{
			Id = baseObj.Id;
			ResourceId = baseObj.ResourceId;
			Name = baseObj.Name;
			AIResource = baseObj.AIResource;
		}
	}
}
