namespace AI.Notebook.Common.AI.Image;

public class ImageTag
{
	public string Name { get; set; } = string.Empty;
	public float Confidence { get; set; }
}
public class ImageTaggingResult
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImageTag>? Tags { get; set; } = null;

}
