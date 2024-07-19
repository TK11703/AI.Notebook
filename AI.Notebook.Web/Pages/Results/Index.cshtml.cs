using AI.Notebook.Common.Entities;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Pages.Results
{
    public class IndexModel : PageModel
    {
		private readonly AIResourcesClient _resourceClient;
		private readonly ResultsClient _resultClient;
		private readonly RequestsClient _requestClient;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, ResultsClient resultClient, AIResourcesClient resourceClient, RequestsClient requestClient)
		{
			_logger = logger;
			_resultClient = resultClient;
			PageResult = new PageResult<ResultBase>(CurrentPage, 0);
			PageSubmission = new PageRequest();
			RequestFormModel = new RequestNewModel();
			_resourceClient = resourceClient;
			_requestClient = requestClient;
		}

		[BindProperty(SupportsGet = true)]
		public int CurrentPage { get; set; } = 1;

		[BindProperty]
		public PageRequest PageSubmission { get; set; }

		[BindProperty]
		public PageResult<ResultBase> PageResult { get; set; }

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
				PageResult = await _resultClient.GetAll(PageSubmission);
				foreach (ResultBase model in PageResult.Collection)
				{
					model.ItemUrlPath = $"{await DetermineRedirectPath(model)}/{model.Id}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the result page.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResult<ResultBase>(PageSubmission.PageSize, PageSubmission.Start);
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
				PageResult = await _resultClient.GetAll(PageSubmission);
				foreach (ResultBase model in PageResult.Collection)
				{
					model.ItemUrlPath = $"{await DetermineRedirectPath(model)}/{model.Id}";
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the result page.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResult<ResultBase>(PageSubmission.PageSize, PageSubmission.Start);
			}
			return Page();
		}

		public async Task<IActionResult> OnPostNewRequestAsync(RequestNewModel RequestFormModel)
		{
			ModelState.Clear();
			//ModelState.ClearValidationState(nameof(RequestFormModel));
			if (RequestFormModel != null && TryValidateModel(RequestFormModel))
			{
				dynamic? requestModel = await GetRequestModel(RequestFormModel.ResourceId, RequestFormModel.Name);
				if (requestModel != null)
				{
					try
					{
						int newItemId = await _requestClient.Create(requestModel);
						HttpContext.Session.SetString("Notification", $"The Request was created successfully.");
						string redirectUrl = await DetermineRedirectPath(RequestFormModel.ResourceId);
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
					HttpContext.Session.SetString("Error", $"The Request was not saved successfully, because the resource ID was not valid.");
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

		private async Task<dynamic?> GetRequestModel(int resourceId, string name)
		{
			var aiResourceModel = await _resourceClient.Get(resourceId);
			dynamic? model = null;
			if (aiResourceModel != null)
			{
				switch (aiResourceModel.Name.ToLower())
				{
					case "speech service":
						model = new SpeechRequest() { Name = name, ResourceId = resourceId }; break;
					case "translator":
						model = new TranslatorRequest() { Name = name, ResourceId = resourceId }; break;
					case "computer vision":
						model = new VisionRequest() { Name = name, ResourceId = resourceId }; break;
					case "language":
						model = new LanguageRequest() { Name = name, ResourceId = resourceId }; break;
				}
			}
			return model;
		}

		private async Task<string> DetermineRedirectPath(int resourceId)
		{
			var aiResourceModel = await _resourceClient.Get(resourceId);
			string urlPath = string.Empty;
			if (aiResourceModel != null)
			{
				urlPath = GetRedirectPath(aiResourceModel.Name);
			}
			return urlPath;
		}

		private async Task<string> DetermineRedirectPath(ResultBase model)
		{
			string urlPath = string.Empty;
			if (model != null)
			{
				if (model.AIResource != null && !string.IsNullOrEmpty(model.AIResource.Name))
				{
					urlPath = GetRedirectPath(model.AIResource.Name);
				}
				else
				{
					urlPath = await DetermineRedirectPath(model.ResourceId);
				}
			}
			return urlPath;
		}

		private string GetRedirectPath(string serviceName)
		{
			string urlPath = string.Empty;
			switch (serviceName.ToLower())
			{
				case "speech service":
					urlPath = "./Results/Speech/Item"; break;
				case "translator":
					urlPath = "./Results/Translator/Item"; break;
				case "computer vision":
					urlPath = "./Results/Vision/Item"; break;
				case "language":
					urlPath = "./Results/Language/Item"; break;
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
