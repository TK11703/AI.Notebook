using AI.Notebook.Common.Models;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Pages.Requests
{
    public class ItemModel : PageModel
    {
		private readonly AIResourceClient _resourceClient;
		private readonly RequestClient _requestClient;
		private readonly ResultClient _resultClient;
		private readonly ILogger<ItemModel> _logger;
		public ItemModel(AIResourceClient resourceClient, ILogger<ItemModel> logger, RequestClient requestClient, ResultClient resultClient)
		{
			_resourceClient = resourceClient;
			_logger = logger;
			RequestFormModel = new RequestNewModel();
			_requestClient = requestClient;
			_resultClient = resultClient;
		}

		[FromRoute]
		public int Id { get; set; }

		public IEnumerable<AIResourceModel> AIResources { get; set; } = null!;

		public IEnumerable<ResultModel> Results { get; set; } = null!;

		[Display(Name = "AI Resource List")]
		public SelectList AIResourceList { get; set; } = null!;

		public RequestNewModel RequestFormModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			AIResources = await _resourceClient.GetAll();
			if (AIResources != null)
			{
				AIResourceList = CreateSelectList(AIResources, x => x.Id, x => x.Name);
			}
			if(Id > 0)
			{
				try
				{
					var requestModel = await _requestClient.Get(Id);
					if(requestModel != null)
					{
						RequestFormModel = new RequestNewModel()
						{
							Id = requestModel.Id,
							Name = requestModel.Name,
							ResourceId = requestModel.ResourceId,
							Prompt = requestModel.Input,
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

		public async Task<IActionResult> OnPostSaveAsync(RequestNewModel RequestFormModel)
		{
			if (RequestFormModel != null && ModelState.IsValid)
			{
				RequestModel item = new RequestModel()
				{
					Id = RequestFormModel.Id,
					ResourceId = RequestFormModel.ResourceId,
					Name = RequestFormModel.Name,
					Input = RequestFormModel.Prompt
				};
				try
				{
					if (item.Id.Equals(0))
					{
						int newItemId = await _requestClient.Create(item);
						HttpContext.Session.SetString("Notification", $"The Request was created successfully.");
						return RedirectToPage($"./Item", new { id = newItemId });
					}
					else
					{
						await _requestClient.Update(item);
						HttpContext.Session.SetString("Notification", $"The Request was updated successfully.");
						return RedirectToPage();
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

		public async Task<IActionResult> OnPostDeleteAsync(RequestNewModel RequestFormModel)
		{
			if (RequestFormModel != null && RequestFormModel.Id > 0)
			{
				try
				{
					await _requestClient.Delete(RequestFormModel.Id);
					HttpContext.Session.SetString("Notification", $"The Request was deleted successfully.");
					return RedirectToPage("/Requests/Index");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The Request was not deleted successfully.");
					HttpContext.Session.SetString("Error", $"The Request was not deleted successfully.");
					return RedirectToPage();
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The Request was not deleted successfully.");
				return RedirectToPage();
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
	}
}
