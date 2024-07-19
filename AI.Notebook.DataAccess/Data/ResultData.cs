using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Entities;
using System.Data;
using Dapper;
using AI.Notebook.Common.AI.Text;
using Azure.Core;

namespace AI.Notebook.DataAccess.Data;
public class ResultData : IResultData
{
	private readonly ISqlDataAccess _dataAccess;

	public ResultData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<IEnumerable<ResultBase>> GetByRequestAsync(int requestId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResults_GetByRequest", new { RequestId = requestId });
		if(results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<IEnumerable<ResultBase>> GetByResourceAsync(int resourceId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResults_GetByResource", new { ResourceId = resourceId });
		if (results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultType> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultBase>();
	}

	public async Task<ResultBase?> GetAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<ResultBase, dynamic>("dbo.spResults_Get", new { Id = id });
		if(results != null)
		{
			var result = results.FirstOrDefault();
			if(result != null)
			{
				AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
				result.AIResource = await aiResourceDataItems.GetAsync(result.ResourceId);
				ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
				result.ResultType = await resultTypeDataItems.GetAsync(result.ResultTypeId);
			}
			return result;
		}
		return null;
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

	public async Task<TranslatorResult?> GetTranslatorAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<TranslatorResult, dynamic>("dbo.spResultsTranslator_Get", new { ResultId = resultId });
			if (results != null)
			{
				var result = results.FirstOrDefault();
				if (result != null)
				{
					result.CopyBaseData(resultModel);
					return result;
				}
			}
		}
		return null;
	}

	public async Task<SpeechResult?> GetSpeechAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<SpeechResult, dynamic>("dbo.spResultsSpeech_Get", new { ResultId = resultId });
			if (results != null)
			{
				var result = results.FirstOrDefault();
				if (result != null)
				{
					result.CopyBaseData(resultModel);
					return result;
				}
			}
		}
		return null;
	}

	public async Task<VisionResult?> GetVisionAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<VisionResult, dynamic>("dbo.spResultsVision_Get", new { ResultId = resultId });
			if (results != null)
			{
				var result = results.FirstOrDefault();
				if (result != null)
				{
					result.CopyBaseData(resultModel);
					return result;
				}
			}
		}
		return null;
	}

	public async Task<LanguageResult?> GetLanguageAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<LanguageResult, dynamic>("dbo.spResultsLanguage_Get", new { ResultId = resultId });
			if (results != null)
			{
				var result = results.FirstOrDefault();
				if (result != null)
				{
					result.CopyBaseData(resultModel);
					return result;
				}
			}
		}
		return null;
	}

	public async Task<int> InsertAsync(ResultBase item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@RequestId", item.RequestId);
		p.Add(name: "@ResultTypeId", item.ResultTypeId);
		p.Add(name: "@ResultData", item.ResultData);
		p.Add(name: "@CompletedDt", item.CompletedDt);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResults_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertTranslatorAsync(TranslatorResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResultId", item.ResultId);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@Input", item.Input);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transliterate", item.Transliterate);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@Ssml", item.Ssml);
		p.Add(name: "@SsmlUrl", item.SsmlUrl);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsTranslator_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertSpeechAsync(SpeechResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResultId", item.ResultId);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@AudioData", item.AudioData);
		p.Add(name: "@AudioUrl", item.AudioUrl);
		p.Add(name: "@Ssml", item.Ssml);
		p.Add(name: "@SsmlUrl", item.SsmlUrl);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transcribe", item.Transcribe);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsSpeech_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertVisionAsync(VisionResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResultId", item.ResultId);
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
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsVision_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertLanguageAsync(LanguageResult item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResultId", item.ResultId);
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
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResultsLanguage_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int Update(ResultBase item)
	{
		return _dataAccess.SaveData<dynamic>("dbo.spResults_Update", new { item.Id, item.ResourceId, item.RequestId, item.ResultTypeId, item.ResultData, item.CompletedDt });
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spResults_Delete", p);
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

	public async Task<bool> DeleteTranslatorResultAsync(int id, int resultId)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultId", resultId);
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

	public async Task<bool> DeleteSpeechResultAsync(int id, int resultId)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultId", resultId);
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

	public async Task<bool> DeleteVisionResultAsync(int id, int resultId)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultId", resultId);
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

	public async Task<bool> DeleteLanguageResultAsync(int id, int resultId)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@ResultId", resultId);
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
