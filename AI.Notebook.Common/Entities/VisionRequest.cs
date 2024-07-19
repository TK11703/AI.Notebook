namespace AI.Notebook.Common.Entities;
public class VisionRequest : RequestBase
{
	public int RequestId { get; set; }
	public string? ImageUrl { get; set; }
	public byte[]? ImageData { get; set; }
	public bool GenderNeutralCaption { get; set; } = false;
	public bool Caption { get; set; } = false;
	public bool DenseCaptions { get; set; } = false;
	public bool Tags { get; set; } = false;
	public bool ObjectDetection { get; set; } = false;
	public bool SmartCrop { get; set; } = false;
	public bool People { get; set; } = false;
	public bool Ocr { get; set; } = false;
}
