using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Entities;
using System.Data;
using Dapper;

namespace AI.Notebook.DataAccess.Data;
public class ResultData : IResultData
{
	private readonly ISqlDataAccess _dataAccess;

	public ResultData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<PageResult<ResultBase>> GetPagedAsync(PageRequest pageRequest)
	{
		PageResult<ResultBase> results = new PageResult<ResultBase>(pageRequest.PageSize, pageRequest.Start);
		var p = new DynamicParameters();
		p.Add(name: "@SortBy", pageRequest.SortBy);
		p.Add(name: "@SortOrder", pageRequest.SortDirection);
		p.Add(name: "@PageSize", pageRequest.PageSize);
		p.Add(name: "@Start", pageRequest.Start);
		p.Add(name: "@PageSize", pageRequest.PageSize);
		if (!string.IsNullOrEmpty(pageRequest.Filter))
		{
			p.Add(name: "@Search", pageRequest.Filter);
		}
		if (pageRequest.BeginDate.HasValue)
		{
			p.Add(name: "@Begin", pageRequest.BeginDate.Value.Date);
		}
		if (pageRequest.EndDate.HasValue)
		{
			p.Add(name: "@End", pageRequest.EndDate.Value.Date);
		}
		p.Add(name: "@Total", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		var resultSubset = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResults_GetPaged", p);
		if (resultSubset != null)
		{
			results.ItemCount = p.Get<int>("@Total");
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var item in resultSubset)
			{
				item.AIResource = aiResources.First(x => x.Id == item.ResourceId);
				item.ResultType = resultTypes.First(x => x.Id == item.ResultTypeId);
			}
			results.Collection = resultSubset;
			return results;
		}

		return new PageResult<ResultBase>();
	}

	public async Task<IEnumerable<ResultBase>?> GetTranslatorResultsByRequestAsync(int requestId)
	{
		var resultSubset = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResultsTranslator_GetByRequest", new { RequestId = requestId });
		if (resultSubset != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var item in resultSubset)
			{
				item.AIResource = aiResources.First(x => x.Id == item.ResourceId);
				item.ResultType = resultTypes.First(x => x.Id == item.ResultTypeId);
			}
		}
		return resultSubset;
	}

	public async Task<IEnumerable<ResultBase>?> GetVisionResultsByRequestAsync(int requestId)
	{
		var resultSubset = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResultsVision_GetByRequest", new { RequestId = requestId });
		if (resultSubset != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var item in resultSubset)
			{
				item.AIResource = aiResources.First(x => x.Id == item.ResourceId);
				item.ResultType = resultTypes.First(x => x.Id == item.ResultTypeId);
			}
		}
		return resultSubset;
	}

	public async Task<IEnumerable<ResultBase>?> GetSpeechResultsByRequestAsync(int requestId)
	{
		var resultSubset = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResultsSpeech_GetByRequest", new { RequestId = requestId });
		if (resultSubset != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var item in resultSubset)
			{
				item.AIResource = aiResources.First(x => x.Id == item.ResourceId);
				item.ResultType = resultTypes.First(x => x.Id == item.ResultTypeId);
			}
		}
		return resultSubset;
	}

	public async Task<IEnumerable<ResultBase>?> GetLanguageResultsByRequestAsync(int requestId)
	{
		var resultSubset = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResultsLanguage_GetByRequest", new { RequestId = requestId });
		if (resultSubset != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var item in resultSubset)
			{
				item.AIResource = aiResources.First(x => x.Id == item.ResourceId);
				item.ResultType = resultTypes.First(x => x.Id == item.ResultTypeId);
			}
		}
		return resultSubset;
	}

	public async Task<TranslatorResult?> GetTranslatorAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<TranslatorResult, dynamic>("dbo.spResultsTranslator_Get", new { Id = id });
		if (results != null)
		{
			var result = results.FirstOrDefault();
			if (result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
				return result;
			}
		}
		return null;
	}

	public async Task<SpeechResult?> GetSpeechAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<SpeechResult, dynamic>("dbo.spResultsSpeech_Get", new { Id = id });
		if (results != null)
		{
			var result = results.FirstOrDefault();
			if (result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
				return result;
			}
		}
		return null;
	}

	public async Task<VisionResult?> GetVisionAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<VisionResult, dynamic>("dbo.spResultsVision_Get", new { Id = id });
		if (results != null)
		{
			var result = results.FirstOrDefault();
			if (result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
				return result;
			}
		}
		return null;
	}

	public async Task<LanguageResult?> GetLanguageAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<LanguageResult, dynamic>("dbo.spResultsLanguage_Get", new { Id = id });
		if (results != null)
		{
			var result = results.FirstOrDefault();
			if (result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
				return result;
			}
		}
		return null;
	}

	public async Task<int> InsertTranslatorAsync(TranslatorResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@Input", item.Input);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transliterate", item.Transliterate);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@ResultText", item.ResultText);
		p.Add(name: "@ResultAudio", item.ResultAudio);
		p.Add(name: "@CompletedDt", item.CompletedDt);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsTranslator_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertSpeechAsync(SpeechResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@AudioData", item.AudioData);
		p.Add(name: "@AudioUrl", item.AudioUrl);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transcribe", item.Transcribe);
		p.Add(name: "@ResultText", item.ResultText);
		p.Add(name: "@ResultAudio", item.ResultAudio);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsSpeech_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertVisionAsync(VisionResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@ImageData", item.ImageData);
		p.Add(name: "@ImageUrl", item.ImageUrl);
		p.Add(name: "@GenderNeutralCaption", item.GenderNeutralCaption);
		p.Add(name: "@Caption", item.Caption);
		p.Add(name: "@DenseCaptions", item.DenseCaptions);
		p.Add(name: "@Tags", item.Tags);
		p.Add(name: "@ObjectDetection", item.ObjectDetection);
		p.Add(name: "@People", item.People);
		p.Add(name: "@SmartCrop", item.SmartCrop);
		p.Add(name: "@Ocr", item.Ocr);
		p.Add(name: "@ResultText", item.ResultText);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsVision_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertLanguageAsync(LanguageResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@Input", item.Input);
		p.Add(name: "@Language", item.Language);
		p.Add(name: "@Sentiment", item.Sentiment);
		p.Add(name: "@KeyPhrases", item.KeyPhrases);
		p.Add(name: "@Entities", item.Entities);
		p.Add(name: "@PiiEntites", item.PiiEntites);
		p.Add(name: "@LinkedEntities", item.LinkedEntities);
		p.Add(name: "@NamedEntityRecognition", item.NamedEntityRecognition);
		p.Add(name: "@Summary", item.Summary);
		p.Add(name: "@AbstractiveSummary", item.AbstractiveSummary);
		p.Add(name: "@ResultText", item.ResultText);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsLanguage_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int UpdateVision(int id, int resultTypeId, string resultText, DateTime? completedDate)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultTypeId", resultTypeId);
		p.Add(name: "@ResultText", resultText);
		p.Add(name: "@CompletedDt", completedDate);
		return _dataAccess.SaveData<DynamicParameters>("dbo.spResultsVision_Update", p);
	}

	public int UpdateSpeech(int id, int resultTypeId, string resultText, byte[] resultAudio, DateTime? completedDate)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultTypeId", resultTypeId);
		p.Add(name: "@ResultText", resultText);
		p.Add(name: "@ResultAudio", resultAudio);
		p.Add(name: "@CompletedDt", completedDate);
		return _dataAccess.SaveData<DynamicParameters>("dbo.spResultsSpeech_Update", p);
	}

	public int UpdateLanguage(int id, int resultTypeId, string resultText, DateTime? completedDate)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultTypeId", resultTypeId);
		p.Add(name: "@ResultText", resultText);
		p.Add(name: "@CompletedDt", completedDate);
		return _dataAccess.SaveData<DynamicParameters>("dbo.spResultsLanguage_Update", p);
	}

	public int UpdateTranslator(int id, int resultTypeId, string resultText, byte[] resultAudio, DateTime? completedDate)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultTypeId", resultTypeId);
		p.Add(name: "@ResultText", resultText);
		p.Add(name: "@ResultAudio", resultAudio);
		p.Add(name: "@CompletedDt", completedDate);
		return _dataAccess.SaveData<DynamicParameters>("dbo.spResultsTranslator_Update", p);
	}

	public async Task<bool> DeleteTranslatorResultAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsTranslator_Delete", p);
		var completed = p.Get<int?>("@Output");
		if (completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public async Task<bool> DeleteSpeechResultAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsSpeech_Delete", p);
		var completed = p.Get<int?>("@Output");
		if (completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public async Task<bool> DeleteVisionResultAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsVision_Delete", p);
		var completed = p.Get<int?>("@Output");
		if (completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public async Task<bool> DeleteLanguageResultAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsLanguage_Delete", p);
		var completed = p.Get<int?>("@Output");
		if (completed.HasValue && completed.Value.Equals(1))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
