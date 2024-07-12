﻿using AI.Notebook.Common.AI.Text;

namespace AI.Notebook.API.Processors;

public class ResultProcessor
{
	public ResultProcessor()
	{

	}

	public async Task<ResultTranslatorModel?> SaveTranslatorResultAsync(TextTranslationResult result, RequestTranslatorModel aRequestModel, IResultData resultData, IResultTypeData resultTypeData)
	{
		ResultTypeModel resultType = null!;
		if (aRequestModel.OutputAsAudio)
		{
			resultType = await resultTypeData.GetByNameAsync("Binary/Audio");
		}
		else
		{
			resultType = await resultTypeData.GetByNameAsync("Plain Text");
		}
		if (resultType == null)
		{
			Exception ex = new("Result type not found");
			throw ex;
		}
		int resultId = await resultData.InsertAsync(new ResultModel()
		{
			RequestId = aRequestModel.RequestId,
			ResourceId = aRequestModel.ResourceId,
			ResultTypeId = resultType.Id,
			CompletedDt = DateTime.Now,
			ResultData = result.GetResultData()
		});
		if (resultId.Equals(0))
		{
			Exception ex = new("Failed to save result");
			throw ex;
		}
		ResultTranslatorModel resultModel = new ResultTranslatorModel()
		{
			ResultId = resultId,
			SourceLangCode = aRequestModel.SourceLangCode,
			TargetLangCode = aRequestModel.TargetLangCode,
			Input = aRequestModel.Input,
			Translate = aRequestModel.Translate,
			Transliterate = aRequestModel.Transliterate,
			OutputAsAudio = aRequestModel.OutputAsAudio,
			Ssml = aRequestModel.Ssml,
			SsmlUrl = aRequestModel.SsmlUrl,			
			VoiceName = aRequestModel.VoiceName
		};
		int translatorModelId = await resultData.InsertTranslatorAsync(resultModel);
		if (translatorModelId.Equals(0))
		{
			Exception ex = new("Failed to save translation association data");
			throw ex;
		}
		resultModel.Id = translatorModelId;
		return resultModel;
	}
}