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

		public async Task<IActionResult> OnPostDeleteAsync(int Id, int ResultId)
		{
			if (Id > 0)
			{
				try
				{
					var completed = await _translatorClient.Delete(Id, ResultId);
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

		private async Task PopulateModelAsync(TranslatorResult result)
		{	
			ResultModel = new TranslatorResultModel()
			{
				Id = result.Id,
				ResultId = result.ResultId,
				ResourceId = result.ResourceId,
				AIResource = result.AIResource,
				ResultTypeId = result.ResultTypeId,
				ResultType = result.ResultType,
				ResultData = result.ResultData,
				CompletedDt = result.CompletedDt,

				Prompt = result.Input,
				//SourceLanguage = result.SourceLangCode,
				//TargetLanguage = result.TargetLangCode,
				Translate = result.Translate,
				Transliterate = result.Transliterate,
				OutputAsAudio = result.OutputAsAudio,
				VoiceName = result.VoiceName,
				Ssml = result.Ssml,
				SsmlUrl = result.SsmlUrl,

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
