namespace AI.Notebook.Common.AI.Image;
public class ImagePerson
{
	public float Confidence { get; set; }

	public string BoundingBox { get; set; } = string.Empty;

}

public class ImagePeopleResult
{
	public int ImageWidth { get; set; }
	public int ImageHeight { get; set; }

	public List<ImagePerson>? People { get; set; } = null;

}
