﻿namespace AI.Notebook.Common.Entities;

public class AIResource
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool Active { get; set; }
}
