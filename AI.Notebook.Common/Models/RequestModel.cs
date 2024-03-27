
namespace AI.Notebook.Common.Models;

public class RequestModel
{
	public int Id { get; set; }
	public int ResourceId { get; set; }
	public AIResourceModel? AIResource { get; set; }
	public string Name { get; set; }
	public string Input { get; set; }
	public DateTime CreatedDt { get; set; }
	public DateTime UpdatedDt { get; set; }
}
