namespace AI.Notebook.Common.AI.Image;
public class ImageWord
{
	public string Text { get; set; } = string.Empty;

	public float Confidence { get; set; }

	public string BoundingBox { get; set; } = string.Empty;

}

public class ImageOcrResult
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImageWord>? Words { get; set; } = null;

}
