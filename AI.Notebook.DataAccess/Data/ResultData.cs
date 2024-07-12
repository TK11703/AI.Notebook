using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Models;
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

	public async Task<IEnumerable<ResultModel>> GetByRequestAsync(int requestId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_GetByRequest", new { RequestId = requestId });
		if(results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultTypeModel> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<IEnumerable<ResultModel>> GetByResourceAsync(int resourceId)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_GetByResource", new { ResourceId = resourceId });
		if (results != null)
		{
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResourceModel> aiResources = await aiResourceDataItems.GetAllAsync();
			ResultTypeData resultTypeDataItems = new ResultTypeData(_dataAccess);
			IEnumerable<ResultTypeModel> resultTypes = await resultTypeDataItems.GetAllAsync();
			foreach (var result in results)
			{
				result.AIResource = aiResources.First(x => x.Id.Equals(result.ResourceId));
				result.ResultType = resultTypes.First(x => x.Id.Equals(result.ResultTypeId));
			}
			return results;
		}
		return Enumerable.Empty<ResultModel>();
	}

	public async Task<ResultModel?> GetAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<ResultModel, dynamic>("dbo.spResults_Get", new { Id = id });
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

	public async Task<ResultTranslatorModel?> GetTranslatorAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<ResultTranslatorModel, dynamic>("dbo.spResultsTranslator_Get", new { ResultId = resultId });
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

	public async Task<ResultSpeechModel?> GetSpeechAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<ResultSpeechModel, dynamic>("dbo.spResultsSpeech_Get", new { ResultId = resultId });
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

	public async Task<ResultVisionModel?> GetVisionAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<ResultVisionModel, dynamic>("dbo.spResultsVision_Get", new { ResultId = resultId });
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

	public async Task<ResultLanguageModel?> GetLanguageAsync(int resultId)
	{
		var resultModel = await GetAsync(resultId);
		if (resultModel != null && resultModel.Id > 0)
		{
			var results = await _dataAccess.LoadDataAsync<ResultLanguageModel, dynamic>("dbo.spResultsLanguage_Get", new { ResultId = resultId });
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

	public async Task<int> InsertAsync(ResultModel item)
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

	public async Task<int> InsertTranslatorAsync(ResultTranslatorModel item)
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

	public async Task<int> InsertSpeechAsync(ResultSpeechModel item)
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

	public async Task<int> InsertVisionAsync(ResultVisionModel item)
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

	public async Task<int> InsertLanguageAsync(ResultLanguageModel item)
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

	public int Update(ResultModel item)
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
}
