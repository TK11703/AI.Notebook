using Azure;
using Azure.AI.Vision.ImageAnalysis;


namespace AI.Notebook.Common.AI.Image;
public class ImageServices
{
	private readonly ImageAnalysisClient _analysisClient;

	public ImageServices(string serviceKey, string endPointUrl)
	{
		_analysisClient = new ImageAnalysisClient(new Uri(endPointUrl), new AzureKeyCredential(serviceKey));
	}

	public async Task<ImageCaptionResult?> GetCaption(byte[] imageFile, bool genderNeutralCaption = true)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.Caption, new ImageAnalysisOptions { GenderNeutralCaption = genderNeutralCaption });
			return new ImageCaptionResult()
			{
				Caption = analysisResult.Caption.Text,
				Confidence = analysisResult.Caption.Confidence,
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};
		}
		return null;
	}

	public async Task<ImageCaptionResult?> GetCaption(string imageUrl, bool genderNeutralCaption = true)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.Caption, new ImageAnalysisOptions { GenderNeutralCaption = genderNeutralCaption });
			return new ImageCaptionResult()
			{
				Caption = analysisResult.Caption.Text,
				Confidence = analysisResult.Caption.Confidence,
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};
		}
		return null;
	}

	public async Task<ImageCaptionResult?> GetDenseCaptions(byte[] imageFile, bool genderNeutralCaption = true)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.DenseCaptions, new ImageAnalysisOptions { GenderNeutralCaption = genderNeutralCaption });
			ImageCaptionResult captionResult = new ImageCaptionResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.DenseCaptions.Values != null)
			{
				ProcessDenseCaptions(analysisResult.DenseCaptions.Values, captionResult);
			}

			return captionResult;
		}
		return null;
	}

	public async Task<ImageCaptionResult?> GetDenseCaptions(string imageUrl, bool genderNeutralCaption = true)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.DenseCaptions, new ImageAnalysisOptions { GenderNeutralCaption = genderNeutralCaption });
			ImageCaptionResult captionResult = new ImageCaptionResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.DenseCaptions.Values != null)
			{
				ProcessDenseCaptions(analysisResult.DenseCaptions.Values, captionResult);
			}
			return captionResult;
		}
		return null;
	}

	private void ProcessDenseCaptions(IReadOnlyList<DenseCaption> denseCaptions, ImageCaptionResult captionResult)
	{
		if (denseCaptions.Count > 0)
		{
			captionResult.DenseCaptions = new List<ImageCaptionBase>();
			foreach (DenseCaption denseCap in denseCaptions)
			{
				captionResult.DenseCaptions.Add(new ImageCaptionBase()
				{
					Caption = denseCap.Text,
					Confidence = denseCap.Confidence,
					BoundingBox = denseCap.BoundingBox.ToString()
				});
			}
		}
	}

	public async Task<ImageTaggingResult?> GetTags(string imageUrl)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.Tags);
			ImageTaggingResult taggingResult = new ImageTaggingResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Tags.Values != null)
			{
				ProcessTags(analysisResult.Tags.Values, taggingResult);
			}
			return taggingResult;
		}
		return null;
	}

	public async Task<ImageTaggingResult?> GetTags(byte[] imageFile)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.Tags);
			ImageTaggingResult taggingResult = new ImageTaggingResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Tags.Values != null)
			{
				ProcessTags(analysisResult.Tags.Values, taggingResult);
			}
			return taggingResult;
		}
		return null;
	}

	private void ProcessTags(IReadOnlyList<DetectedTag> tags, ImageTaggingResult taggingResult)
	{
		if (tags.Count > 0)
		{
			taggingResult.Tags = new List<ImageTag>();
			foreach (DetectedTag imageTag in tags)
			{
				taggingResult.Tags.Add(new ImageTag()
				{
					Name = imageTag.Name,
					Confidence = imageTag.Confidence
				});
			}
		}
	}

	public async Task<ImageObjectResult?> GetObjects(string imageUrl)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.Objects);
			ImageObjectResult objectResult = new ImageObjectResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Objects.Values != null)
			{
				ProcessObjects(analysisResult.Objects.Values, objectResult);
			}
			return objectResult;
		}
		return null;
	}

	public async Task<ImageObjectResult?> GetObjects(byte[] imageFile)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.Objects);
			ImageObjectResult objectResult = new ImageObjectResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Objects.Values != null)
			{
				ProcessObjects(analysisResult.Objects.Values, objectResult);
			}
			return objectResult;
		}
		return null;
	}

	private void ProcessObjects(IReadOnlyList<DetectedObject> objects, ImageObjectResult objectResult)
	{
		if (objects.Count > 0)
		{
			objectResult.Objects = new List<ImageObject>();
			foreach (DetectedObject imageObj in objects)
			{
				objectResult.Objects.Add(new ImageObject()
				{
					Name = imageObj.Tags.First().Name,
					BoundingBox = imageObj.BoundingBox.ToString()
				});
			}
		}
	}

	public async Task<ImagePeopleResult?> GetPeople(string imageUrl)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.People);
			ImagePeopleResult peopleResult = new ImagePeopleResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.People.Values != null)
			{
				ProcessPeople(analysisResult.People.Values, peopleResult);
			}
			return peopleResult;
		}
		return null;
	}

	public async Task<ImagePeopleResult?> GetPeople(byte[] imageFile)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.People);
			ImagePeopleResult peopleResult = new ImagePeopleResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.People.Values != null)
			{
				ProcessPeople(analysisResult.People.Values, peopleResult);
			}
			return peopleResult;
		}
		return null;
	}

	private void ProcessPeople(IReadOnlyList<DetectedPerson> peopleList, ImagePeopleResult peopleResult)
	{
		if (peopleList.Count > 0)
		{
			peopleResult.People = new List<ImagePerson>();
			foreach (DetectedPerson personObj in peopleList)
			{
				peopleResult.People.Add(new ImagePerson()
				{
					Confidence = personObj.Confidence,
					BoundingBox = personObj.BoundingBox.ToString()
				});
			}
		}
	}

	public async Task<ImageOcrResult?> PerformOCR(string imageUrl)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.Read);
			ImageOcrResult ocrResult = new ImageOcrResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Read.Blocks != null)
			{
				ProcessOcrTextBlocks(analysisResult.Read.Blocks, ocrResult);
			}
			return ocrResult;
		}
		return null;
	}

	public async Task<ImageOcrResult?> PerformOCR(byte[] imageFile)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.Read);
			ImageOcrResult ocrResult = new ImageOcrResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.Read.Blocks != null)
			{
				ProcessOcrTextBlocks(analysisResult.Read.Blocks, ocrResult);
			}
			return ocrResult;
		}
		return null;
	}

	private void ProcessOcrTextBlocks(IReadOnlyList<DetectedTextBlock> textBlocks, ImageOcrResult ocrResult)
	{
		if (textBlocks.Count > 0)
		{
			ocrResult.Words = new List<ImageWord>();
			foreach (var line in textBlocks.SelectMany(block => block.Lines))
			{
				foreach (DetectedTextWord word in line.Words)
				{
					ocrResult.Words.Add(new ImageWord()
					{
						Text = word.Text,
						BoundingBox = string.Join(" ", word.BoundingPolygon),
						Confidence = word.Confidence
					});
				}
			}
		}
	}

	public async Task<ImageSmartCropResult?> GetSmartCrops(string imageUrl)
	{
		if (!string.IsNullOrEmpty(imageUrl))
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.SmartCrops);
			ImageSmartCropResult smartCropResult = new ImageSmartCropResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.SmartCrops != null)
			{
				ProcessSmartCrops(analysisResult.SmartCrops.Values, smartCropResult);
			}
			return smartCropResult;
		}
		return null;
	}

	public async Task<ImageSmartCropResult?> GetSmartCrops(byte[] imageFile)
	{
		if (imageFile.Length > 0)
		{
			ImageAnalysisResult analysisResult = await _analysisClient.AnalyzeAsync(BinaryData.FromBytes(imageFile), VisualFeatures.SmartCrops);
			ImageSmartCropResult smartCropResult = new ImageSmartCropResult()
			{
				ImageHeight = analysisResult.Metadata.Height,
				ImageWidth = analysisResult.Metadata.Width
			};

			if (analysisResult.SmartCrops != null)
			{
				ProcessSmartCrops(analysisResult.SmartCrops.Values, smartCropResult);
			}
			return smartCropResult;
		}
		return null;
	}

	private void ProcessSmartCrops(IReadOnlyList<CropRegion> cropRegions, ImageSmartCropResult smartCropResult)
	{
		if (cropRegions.Count > 0)
		{
			smartCropResult.SmartCrops = new List<ImageCrop>();
			foreach (var cropRegion in cropRegions)
			{
				smartCropResult.SmartCrops.Add(new ImageCrop()
				{
					BoundingBox = cropRegion.BoundingBox.ToString(),
					AspectRatio = cropRegion.AspectRatio
				});
			}
		}
	}
}
