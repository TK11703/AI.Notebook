﻿using AI.Notebook.Common.AI.Text;

namespace AI.Notebook.API.Processors;

public class ResultProcessor
{
	public ResultProcessor()
	{

	}

	public async Task<TranslatorResult?> SaveTranslatorResultAsync(TextTranslationResult result, TranslatorRequest aRequestModel, IResultData resultData, IResultTypeData resultTypeData)
	{
		ResultType resultType = null!;
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
		TranslatorResult resultModel = new TranslatorResult()
		{
			RequestId = aRequestModel.Id,
			ResourceId = aRequestModel.ResourceId,
			ResultTypeId = resultType.Id,
			SourceLangCode = aRequestModel.SourceLangCode,
			TargetLangCode = aRequestModel.TargetLangCode,
			Input = aRequestModel.Input,
			Translate = aRequestModel.Translate,
			Transliterate = aRequestModel.Transliterate,
			OutputAsAudio = aRequestModel.OutputAsAudio,
			CompletedDt = DateTime.Now,
			ResultText = result.Output,
			ResultAudio = result.AudioOutput,
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

	public async Task<TranslatorResult>? SaveTransliterationResultAsync(TextTransliterationResult result, TranslatorRequest aRequestModel, IResultData resultData, IResultTypeData resultTypeData)
	{
		ResultType resultType = await resultTypeData.GetByNameAsync("Plain Text");
		if (resultType == null)
		{
			Exception ex = new("Result type not found");
			throw ex;
		}
		TranslatorResult resultModel = new TranslatorResult()
		{
			RequestId = aRequestModel.Id,
			ResourceId = aRequestModel.ResourceId,
			ResultTypeId = resultType.Id,
			SourceLangCode = aRequestModel.SourceLangCode,
			SourceScriptCode = aRequestModel.SourceScriptCode,
			TargetScriptCode = aRequestModel.TargetScriptCode,
			Input = aRequestModel.Input,
			Translate = aRequestModel.Translate,
			Transliterate = aRequestModel.Transliterate,
			CompletedDt = DateTime.Now,
			ResultText = result.Output,
			ResultAudio = new byte[0]
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
