using AI.Notebook.DataAccess.DBAccess;
using AI.Notebook.Common.Entities;
using Dapper;
using System.Data;

namespace AI.Notebook.DataAccess.Data;
public class RequestData : IRequestData
{
	private readonly ISqlDataAccess _dataAccess;

	public RequestData(ISqlDataAccess dataAccess)
	{
		_dataAccess = dataAccess;
	}

	public async Task<PageResult<RequestBase>> GetPagedAsync(PageRequest pageRequest)
	{
		PageResult<RequestBase> results = new PageResult<RequestBase>(pageRequest.PageSize, pageRequest.Start);
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
		var requests = await _dataAccess.LoadDataAsync<RequestBase, dynamic>("dbo.spRequests_GetPaged", p);
		if (requests != null)
		{
			results.ItemCount = p.Get<int>("@Total");
			AIResourceData aiResourceDataItems = new AIResourceData(_dataAccess);
			IEnumerable<AIResource> aiResources = await aiResourceDataItems.GetAllAsync();
			foreach (var request in requests)
			{
				request.AIResource = aiResources.First(x => x.Id == request.ResourceId);
			}
			results.Collection = requests;
			return results;
		}

		return new PageResult<RequestBase>();
	}

	public async Task<TranslatorRequest?> GetTranslatorAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<TranslatorRequest, dynamic>("dbo.spRequestsTranslator_Get", new { Id = id });
		if (results != null)
		{
			var request = results.FirstOrDefault();
			if (request != null)
			{
				AIResourceData data = new AIResourceData(_dataAccess);
				request.AIResource = await data.GetAsync(request.ResourceId);
				return request;
			}
		}
		return null;
	}

	public async Task<SpeechRequest?> GetSpeechAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<SpeechRequest, dynamic>("dbo.spRequestsSpeech_Get", new { Id = id });
		if (results != null)
		{
			var request = results.FirstOrDefault();
			if (request != null)
			{
				AIResourceData data = new AIResourceData(_dataAccess);
				request.AIResource = await data.GetAsync(request.ResourceId);
				return request;
			}
		}
		return null;
	}

	public async Task<VisionRequest?> GetVisionAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<VisionRequest, dynamic>("dbo.spRequestsVision_Get", new { Id = id });
		if (results != null)
		{
			var request = results.FirstOrDefault();
			if (request != null)
			{
				AIResourceData data = new AIResourceData(_dataAccess);
				request.AIResource = await data.GetAsync(request.ResourceId);
				return request;
			}
		}
		return null;
	}

	public async Task<LanguageRequest?> GetLanguageAsync(int id)
	{
		var results = await _dataAccess.LoadDataAsync<LanguageRequest, dynamic>("dbo.spRequestsLanguage_Get", new { Id = id });
		if (results != null)
		{
			var request = results.FirstOrDefault();
			if (request != null)
			{
				AIResourceData data = new AIResourceData(_dataAccess);
				request.AIResource = await data.GetAsync(request.ResourceId);
				return request;
			}
		}
		return null;
	}

	public async Task<int> InsertTranslatorAsync(TranslatorRequest item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@Name", item.Name);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@Input", item.Input);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transliterate", item.Transliterate);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsTranslator_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertSpeechAsync(SpeechRequest item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@Name", item.Name);
		p.Add(name: "@SourceLangCode", item.SourceLangCode);
		p.Add(name: "@TargetLangCode", item.TargetLangCode);
		p.Add(name: "@AudioData", item.AudioData);
		p.Add(name: "@AudioUrl", item.AudioUrl);
		p.Add(name: "@OutputAsAudio", item.OutputAsAudio);
		p.Add(name: "@VoiceName", item.VoiceName);
		p.Add(name: "@Translate", item.Translate);
		p.Add(name: "@Transcribe", item.Transcribe);
		p.Add(name: "@Id", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsSpeech_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertVisionAsync(VisionRequest item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@Name", item.Name);
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

		await _dataAccess.SaveDataAsync("dbo.spRequestsVision_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public async Task<int> InsertLanguageAsync(LanguageRequest item)
	{
		var p = new DynamicParameters();
		p.Add(name: "@ResourceId", item.ResourceId);
		p.Add(name: "@Name", item.Name);
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

		await _dataAccess.SaveDataAsync("dbo.spRequestsLanguage_Insert", p);
		var newId = p.Get<int?>("@Id");
		return newId.HasValue ? newId.Value : 0;
	}

	public int UpdateTranslator(TranslatorRequest item)
	{
		dynamic parameters = new { item.Name, item.Id, item.SourceLangCode, item.TargetLangCode, item.Input, item.Translate, item.Transliterate, item.OutputAsAudio, item.VoiceName };
		return _dataAccess.SaveData<dynamic>("dbo.spRequestsTranslator_Update", parameters);
	}

	public int UpdateSpeech(SpeechRequest item)
	{
		dynamic parameters = new { item.Name, item.Id, item.SourceLangCode, item.TargetLangCode, item.AudioData, item.AudioUrl, item.OutputAsAudio, item.VoiceName, item.Translate, item.Transcribe };
		return _dataAccess.SaveData<dynamic>("dbo.spRequestsSpeech_Update", parameters);
	}

	public int UpdateVision(VisionRequest item)
	{
		dynamic parameters = new { item.Name, item.Id, item.ImageData, item.ImageUrl, item.GenderNeutralCaption, item.Caption, item.DenseCaptions, item.Tags, item.ObjectDetection, item.People, item.SmartCrop, item.Ocr };
		return _dataAccess.SaveData<dynamic>("dbo.spRequestsVision_Update", parameters);
	}

	public int UpdateLanguage(LanguageRequest item)
	{
		dynamic parameters = new { item.Name, item.Id, item.SourceLangCode, item.TargetLangCode, item.Input, item.Language, item.Sentiment, item.KeyPhrases, item.Entities, item.PiiEntites, item.LinkedEntities, item.NamedEntityRecognition, item.Summary, item.AbstractiveSummary };
		return _dataAccess.SaveData<dynamic>("dbo.spRequestsLanguage_Update", parameters);
	}

	public async Task<bool> DeleteLanguageAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsLanguage_Delete", p);
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

	public async Task<bool> DeleteSpeechAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsSpeech_Delete", p);
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

	public async Task<bool> DeleteVisionAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsVision_Delete", p);
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

	public async Task<bool> DeleteTranslatorAsync(int id)
	{
		var p = new DynamicParameters();
		p.Add(name: "@Id", id);
		p.Add(name: "@Output", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		await _dataAccess.SaveDataAsync("dbo.spRequestsTranslator_Delete", p);
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
