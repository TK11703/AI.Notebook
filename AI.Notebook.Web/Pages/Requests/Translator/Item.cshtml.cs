using AI.Notebook.Common.AI.Text;
using AI.Notebook.Common.Models;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Pages.Requests.Translator
{
    public class ItemModel : PageModel
    {
		private readonly AIResourceClient _resourceClient;
		private readonly RequestClient _requestClient;
		private readonly ResultClient _resultClient;
		private readonly ServicesClient _servicesClient;
		private readonly ILogger<ItemModel> _logger;
		public ItemModel(AIResourceClient resourceClient, ILogger<ItemModel> logger, RequestClient requestClient, ResultClient resultClient, ServicesClient servicesClient)
		{
			_resourceClient = resourceClient;
			_logger = logger;			
			_requestClient = requestClient;
			_resultClient = resultClient;
			_servicesClient = servicesClient;
			RequestFormModel = new TranslatorModel();
		}

		[FromRoute]
		public int Id { get; set; }

		[Display(Name = "AI Resource List")]
		public SelectList AIResourceList { get; set; } = null!;

		[Display(Name = "Source Language")]
		public SelectList? SourceLanguageList { get; set; }

		[Display(Name = "Target Language")]
		public SelectList? TargetLanguageList { get; set; }

		public IEnumerable<ResultModel> Results { get; set; } = null!;

		public TranslatorModel RequestFormModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			await BuildLists();
			if (Id > 0)
			{
				try
				{
					var requestModel = await _requestClient.GetTranslatorModel(Id);
					if(requestModel != null)
					{
						RequestFormModel = new TranslatorModel()
						{
							Id = requestModel.Id,
							Name = requestModel.Name,
							ResourceId = requestModel.ResourceId,
							Prompt = requestModel.Input,
							TargetLanguage = requestModel.TargetLangCode,
							SourceLanguage = requestModel.SourceLangCode,
							Translate = requestModel.Translate,
							Transliterate = requestModel.Transliterate,
							OutputAsAudio = requestModel.OutputAsAudio,
							Ssml = requestModel.Ssml,
							SsmlUrl = requestModel.SsmlUrl,
							VoiceName = requestModel.VoiceName,
							CreatedDt = requestModel.CreatedDt,
							UpdatedDt = requestModel.UpdatedDt
						};

						Results = await _resultClient.GetByRequest(Id);
					}
					else
					{
						HttpContext.Session.SetString("Error", $"The Request was not retrieved successfully.");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The Request was not retrieved successfully.");
					HttpContext.Session.SetString("Error", $"The Request was not retrieved successfully.");
					return RedirectToPage();
				}
			}

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync(TranslatorModel RequestFormModel)
		{
			if (RequestFormModel != null && ModelState.IsValid)
			{
				RequestTranslatorModel item = new RequestTranslatorModel()
				{
					Id = RequestFormModel.Id,
					RequestId = RequestFormModel.Id,
					ResourceId = RequestFormModel.ResourceId,
					Name = RequestFormModel.Name,
					Input = RequestFormModel.Prompt,
					TargetLangCode = RequestFormModel.TargetLanguage,
					SourceLangCode = RequestFormModel.SourceLanguage,
					Translate = RequestFormModel.Translate,
					Transliterate = RequestFormModel.Transliterate,
					VoiceName = RequestFormModel.VoiceName,
					OutputAsAudio = RequestFormModel.OutputAsAudio,
					Ssml = RequestFormModel.Ssml,
					SsmlUrl = RequestFormModel.SsmlUrl
				};
				try
				{
					if (item.Id > 0)
					{
						await _requestClient.Update(item);
						HttpContext.Session.SetString("Notification", $"The Request was updated successfully.");
						return RedirectToPage();
					}
					else
					{
						HttpContext.Session.SetString("Error", $"The identifier was invalid, so the update could not be processed.");
						return BadRequest("The identifier was invalid, so the update could not be processed.");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The Request was not saved successfully.");
					HttpContext.Session.SetString("Error", $"The Request was not saved successfully.");
					return RedirectToPage();
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The Request was not saved successfully.");
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostExecuteAsync(TranslatorModel RequestFormModel)
		{
			if (RequestFormModel != null && ModelState.IsValid)
			{
				RequestTranslatorModel item = new RequestTranslatorModel()
				{
					Id = RequestFormModel.Id,
					RequestId = RequestFormModel.Id,
					ResourceId = RequestFormModel.ResourceId,
					Name = RequestFormModel.Name,
					Input = RequestFormModel.Prompt,
					TargetLangCode = RequestFormModel.TargetLanguage,
					SourceLangCode = RequestFormModel.SourceLanguage,
					Translate = RequestFormModel.Translate,
					Transliterate = RequestFormModel.Transliterate,
					VoiceName = RequestFormModel.VoiceName,
					OutputAsAudio = RequestFormModel.OutputAsAudio,
					Ssml = RequestFormModel.Ssml,
					SsmlUrl = RequestFormModel.SsmlUrl
				};
				try
				{
					if (IsValid(item))
					{
						if (item.Translate)
						{
							await _servicesClient.Translate(item);
						}
						else
						{
							await _servicesClient.Transliterate(item);
						}
						
						HttpContext.Session.SetString("Notification", $"The request was executed successfully.");
						return RedirectToPage();
					}
					else
					{
						HttpContext.Session.SetString("Error", $"The request was invalid, so the execution could not be processed.");
						return BadRequest("The request was invalid, so the execution could not be processed.");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The execution did not completed successfully.");
					HttpContext.Session.SetString("Error", $"The execution did not completed successfully.");
					return RedirectToPage();
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The request sent was not executed successfully, as the item was invalid.");
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostDeleteAsync(int Id)
		{
			if (Id > 0)
			{
				try
				{
					await _requestClient.Delete(Id);
					HttpContext.Session.SetString("Notification", $"The request was deleted successfully.");
					return RedirectToPage("/Requests/Index");
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

		private async Task BuildLists()
		{
			var AIResources = await _resourceClient.GetAll();
			if (AIResources != null)
			{
				AIResourceList = CreateSelectList(AIResources, x => x.Id, x => x.Name);
			}

			SupportedLanguagesResult? langResult = await _servicesClient.GetTranslatorLanguages();
			if (langResult != null && langResult.SupportedLanguages != null)
			{
				SourceLanguageList = CreateSelectList(langResult.SupportedLanguages, x => x.Key, x => x.Name);
				TargetLanguageList = CreateSelectList(langResult.SupportedLanguages, x => x.Key, x => x.Name);
			}
		}

		private SelectList CreateSelectList<T>(IEnumerable<T> itemList, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
		{
			var options = new List<SelectListItem>();
			options.Add(new SelectListItem { Value = "", Text = "Select an item" });

			if (itemList != null)
			{
				options.AddRange(itemList.Select(item => new SelectListItem
				{
					Value = funcToGetValue(item).ToString(),
					Text = funcToGetText(item).ToString()
				}));
			}
			return new SelectList(options, "Value", "Text");
		}

		private bool IsValid(RequestTranslatorModel item)
		{
			bool isValid = true;


			return isValid;
		}
	}
}
