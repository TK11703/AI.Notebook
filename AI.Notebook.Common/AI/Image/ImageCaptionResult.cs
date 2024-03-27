namespace AI.Notebook.Common.AI.Image;
public class ImageCaptionBase
{
	public string Caption { get; set; } = string.Empty;

	public float Confidence { get; set; }

	public string BoundingBox { get; set; } = string.Empty;

}

public class ImageCaptionResult : ImageCaptionBase
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImageCaptionBase>? DenseCaptions { get; set; } = null;

}
