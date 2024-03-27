namespace AI.Notebook.Common.AI.Image;
public class ImageCrop
{
	public float AspectRatio { get; set; }

	public string BoundingBox { get; set; } = string.Empty;

}

public class ImageSmartCropResult
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImageCrop>? SmartCrops { get; set; } = null;

}
