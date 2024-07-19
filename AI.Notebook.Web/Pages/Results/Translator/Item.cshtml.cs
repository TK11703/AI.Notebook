using AI.Notebook.Web.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Models;
using AI.Notebook.Common.AI.Text;

namespace AI.Notebook.Web.Pages.Results.Translator
{
	public class ItemModel : PageModel
	{
		private readonly ILogger<ItemModel> _logger;		
		private readonly TranslatorClient _translatorClient;
		public ItemModel(ILogger<ItemModel> logger, TranslatorClient translatorClient)
		{
			_logger = logger;
			_translatorClient = translatorClient;
			ResultModel = new TranslatorResultModel();
		}

		[FromRoute]
		public int Id { get; set; }

		public TranslatorResultModel ResultModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			if(Id == 0) 
			{
				_logger.LogError("Id is not set. Result lookup cannot be completed.");
				return NotFound();
			}
			
			try
			{
				TranslatorResult? translationResult = await _translatorClient.GetTranslationResult(Id);
				if(translationResult == null) 
				{
					_logger.LogError("Result model with that ID was not found.");
					return NotFound();
				}
				await PopulateModelAsync(translationResult);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting result model");
			}
			return Page();			
		}

		public async Task<IActionResult> OnPostDeleteAsync(int Id)
		{
			if (Id > 0)
			{
				try
				{
					var completed = await _translatorClient.DeleteResult(Id);
					if (completed)
					{
						HttpContext.Session.SetString("Notification", $"The request was deleted successfully.");
						return RedirectToPage("/Results/Index");
					}
					else
					{
						HttpContext.Session.SetString("Error", $"The request was NOT deleted successfully.");
						return Page();
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The request was not deleted successfully.");
					HttpContext.Session.SetString("Error", $"The Request was not deleted successfully.");
					return RedirectToPage();
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The request was not deleted successfully.");
				return RedirectToPage();
			}
		}

		public async Task<IActionResult> OnPostSSMLRequestAsync()
		{
			FormCollection form = (FormCollection)await Request.ReadFormAsync();
			if (form != null)
			{
				if(string.IsNullOrEmpty(form["ResultModel.Ssml"].ToString()))
				{
					HttpContext.Session.SetString("Error", $"The SSML request was not generated successfully.");
					return RedirectToPage();
				}
				try
				{
					SsmlRequest request = new SsmlRequest();
					request.Ssml = form["ResultModel.Ssml"].ToString();
					byte[]? result = await _translatorClient.GenerateSsml(request);
					if (result.Length > 0)
					{
						return File(result, "audio/wav", "audioDownload.wav");
					}
					else
					{
						HttpContext.Session.SetString("Error", $"The SSML request was NOT generated successfully.");
						return RedirectToPage();
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The SSML request was not generated successfully.");
					HttpContext.Session.SetString("Error", $"The SSML Request was not generated successfully.");
					return RedirectToPage();
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The SSML request was not generated successfully.");
				return RedirectToPage();
			}
		}

		private async Task PopulateModelAsync(TranslatorResult result)
		{	
			ResultModel = new TranslatorResultModel()
			{
				Id = result.Id,
				RequestId = result.RequestId,
				ResourceId = result.ResourceId,
				AIResource = result.AIResource,
				ResultTypeId = result.ResultTypeId,
				ResultType = result.ResultType,
				CompletedDt = result.CompletedDt,

				Prompt = result.Input,
				SourceLanguage = result.SourceLangCode,
				TargetLanguage = result.TargetLangCode,
				Translate = result.Translate,
				Transliterate = result.Transliterate,
				OutputAsAudio = result.OutputAsAudio,
				VoiceName = result.VoiceName,
				ResultText = result.ResultText,
				ResultAudio = result.ResultAudio,

				CreatedDt = result.CreatedDt,
				UpdatedDt = result.UpdatedDt
			};

			SupportedLanguagesResult? langResult = await _translatorClient.GetTranslatorLanguages();
			if (langResult != null && langResult.SupportedLanguages != null)
			{
				ResultModel.SourceLanguage = langResult.SupportedLanguages.FirstOrDefault(x => x.Key == result.SourceLangCode)?.Name;
				ResultModel.TargetLanguage = langResult.SupportedLanguages.FirstOrDefault(x => x.Key == result.TargetLangCode)?.Name;
			}
		}
	}
}
