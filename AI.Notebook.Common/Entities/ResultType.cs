namespace AI.Notebook.Common.Entities;

public class ResultType
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool Active { get; set; }
}
