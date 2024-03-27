namespace AI.Notebook.Common.AI.Image;
public class ImageObject
{
	public string Name { get; set; } = string.Empty;

	public string BoundingBox { get; set; } = string.Empty;

}

public class ImageObjectResult
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImageObject>? Objects { get; set; } = null;

}
