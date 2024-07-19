using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AI.Notebook.Web.Pages.Requests
{
	public class IndexModel : PageModel
	{
		private readonly AIResourcesClient _resourceClient;
		private readonly RequestsClient _requestClient;
		private readonly TranslatorClient _translatorClient;
		private readonly SpeechClient _speechClient;
		private readonly VisionClient _visionClient;
		private readonly LanguageClient _languageClient;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, RequestsClient requestClient, AIResourcesClient resourceClient, TranslatorClient translatorClient, SpeechClient speechClient,
			VisionClient visionClient, LanguageClient languageClient)
		{
			_logger = logger;
			_requestClient = requestClient;
			_resourceClient = resourceClient;
			_translatorClient = translatorClient;
			_speechClient = speechClient;
			_languageClient = languageClient;
			_visionClient = visionClient;
			PageResult = new PageResult<RequestBase>(CurrentPage, 0);
			PageSubmission = new PageRequest();
			RequestFormModel = new RequestNewModel();
		}

		[BindProperty(SupportsGet = true)]
		public int CurrentPage { get; set; } = 1;

		[BindProperty]
		public PageRequest PageSubmission { get; set; }

		[BindProperty]
		public PageResult<RequestBase> PageResult { get; set; }

		public IEnumerable<AIResource> AIResources { get; set; } = null!;

		[Display(Name = "AI Resource List")]
		public SelectList AIResourceList { get; set; } = null!;

		public RequestNewModel RequestFormModel { get; set; }

		public async Task OnGetAsync()
		{
			AIResources = await _resourceClient.GetAll();
			if (AIResources != null)
			{
				AIResourceList = CreateSelectList(AIResources, x => x.Id, x => x.Name);
			}

			PageSubmission.SetDefaults(CurrentPage, 10, "Name", "Asc");
			try
			{
				PageResult = await _requestClient.GetAll(PageSubmission);
				foreach (RequestBase model in PageResult.Collection)
				{
					model.ItemUrlPath = $"{GetRedirectPath(model.AIResource.Name)}/{model.Id}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the application list.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResult<RequestBase>(PageSubmission.PageSize, PageSubmission.Start);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			ModelState.Clear();
			if (!TryValidateModel(PageSubmission, nameof(PageSubmission)))
			{
				return Page();
			}

			try
			{
				PageResult = await _requestClient.GetAll(PageSubmission);
				foreach (RequestBase model in PageResult.Collection)
				{
					model.ItemUrlPath = $"{GetRedirectPath(model.AIResource.Name)}/{model.Id}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the application list.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResult<RequestBase>(PageSubmission.PageSize, PageSubmission.Start);
			}
			return Page();
		}

		public async Task<IActionResult> OnPostNewRequestAsync(RequestNewModel RequestFormModel)
		{
			ModelState.Clear();
			//ModelState.ClearValidationState(nameof(RequestFormModel));
			if (RequestFormModel != null && TryValidateModel(RequestFormModel))
			{
				var resourceModel = await _resourceClient.Get(RequestFormModel.ResourceId);
				if (resourceModel == null)
				{
					HttpContext.Session.SetString("Error", $"The Request was not saved successfully, because the resource ID was not valid.");
					return RedirectToPage();
				}
				try
				{
					int newItemId = 0;
					switch (resourceModel.Name.ToLower())
					{
						case "speech service":
							newItemId = await SaveNewSpeechRequest(RequestFormModel.Name, RequestFormModel.ResourceId); break;
						case "translator":
							newItemId = await SaveNewTranslatorRequest(RequestFormModel.Name, RequestFormModel.ResourceId); break;
						case "computer vision":
							newItemId = await SaveNewVisionRequest(RequestFormModel.Name, RequestFormModel.ResourceId); break;
						case "language":
							newItemId = await SaveNewLanguageRequest(RequestFormModel.Name, RequestFormModel.ResourceId); break;
					}
					if (newItemId == 0)
					{
						HttpContext.Session.SetString("Error", $"The Request was not saved successfully.");
						return RedirectToPage();
					}
					string redirectUrl = GetRedirectPath(resourceModel.Name);
					return RedirectToPage($"{redirectUrl}", new { id = newItemId });
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
				return RedirectToPage();
			}
		}

		private async Task<int> SaveNewTranslatorRequest(string name, int resourceId)
		{
			var model = new TranslatorRequest() { Name = name, ResourceId = resourceId };
			return await _translatorClient.Create(model);
		}

		private async Task<int> SaveNewVisionRequest(string name, int resourceId)
		{
			var model = new VisionRequest() { Name = name, ResourceId = resourceId };
			return await _visionClient.Create(model);
		}

		private async Task<int> SaveNewLanguageRequest(string name, int resourceId)
		{
			var model = new LanguageRequest() { Name = name, ResourceId = resourceId };
			return await _languageClient.Create(model);
		}

		private async Task<int> SaveNewSpeechRequest(string name, int resourceId)
		{
			var model = new SpeechRequest() { Name = name, ResourceId = resourceId };
			return await _speechClient.Create(model);
		}

		private string GetRedirectPath(string serviceName)
		{
			string urlPath = string.Empty;
			switch (serviceName.ToLower())
			{
				case "speech service":
					urlPath = "/Requests/Speech/Item"; break;
				case "translator":
					urlPath = "/Requests/Translator/Item"; break;
				case "computer vision":
					urlPath = "/Requests/Vision/Item"; break;
				case "language":
					urlPath = "/Requests/Language/Item"; break;
			}
			return urlPath;
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
	}
}
