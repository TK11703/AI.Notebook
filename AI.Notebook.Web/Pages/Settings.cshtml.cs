using AI.Notebook.Common.Models;
using AI.Notebook.Web.Clients;
using AI.Notebook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Web.Pages
{
    public class SettingsModel : PageModel
    {
		private readonly AIResourceClient _resourceClient;
		private readonly ResultTypeClient _resultTypeClient;
		private readonly ILogger<SettingsModel> _logger;

		public SettingsModel(AIResourceClient resourceClient, ResultTypeClient resultTypeClient, ILogger<SettingsModel> logger)
		{
			_resourceClient = resourceClient;
			_resultTypeClient = resultTypeClient;
			AIResourceFormModel = new SettingsAIResourceModel();
			ResultTypeFormModel = new SettingsResultTypeModel();
			_logger = logger;
		}

		public IEnumerable<AIResourceModel> AIResources { get; set; } = null!;

		public IEnumerable<ResultTypeModel> ResultTypes { get; set; } = null!;

		[Display(Name = "AI Resource List")]
		public SelectList AIResourceList { get; set; } = null!;

		[Display(Name = "Result Type List")]
		public SelectList ResultTypeList { get; set; } = null!;

		public SettingsAIResourceModel AIResourceFormModel { get; set; }

		public SettingsResultTypeModel ResultTypeFormModel { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
			AIResources = await _resourceClient.GetAll();
			if(AIResources != null)
			{
				AIResourceList = CreateSelectList(AIResources, x=>x.Id, x=>x.Name);
			}
			ResultTypes = await _resultTypeClient.GetAll();
			if (ResultTypes != null)
			{
				AIResourceList = CreateSelectList(ResultTypes, x => x.Id, x => x.Name);
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAIResourceSaveAsync(SettingsAIResourceModel AIResourceFormModel)
		{
			if (AIResourceFormModel != null && AIResourceFormModel.IsValid())
			{
				AIResourceModel item = new AIResourceModel()
				{
					Id = AIResourceFormModel.Id,
					Name = AIResourceFormModel.Name,
					Description = AIResourceFormModel.Description,
					Active = true
				};
				try
				{
					if (item.Id.Equals(0))
					{
						await _resourceClient.Create(item);
						HttpContext.Session.SetString("Notification", $"The AI Resource was created successfully.");
					}
					else
					{
						await _resourceClient.Update(item, item.Id);
						HttpContext.Session.SetString("Notification", $"The AI Resource was updated successfully.");
					}
					return RedirectToPage("Settings");
				}
				catch(Exception ex)
				{
					_logger.LogError(ex, "The AI Resource was not saved successfully.");
					HttpContext.Session.SetString("Error", $"The AI Resource was not saved successfully.");
					return RedirectToPage("Settings");
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The AI Resource was not saved successfully.");
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostAIResourceDeleteAsync(SettingsAIResourceModel AIResourceFormModel)
		{
			if (AIResourceFormModel != null && AIResourceFormModel.Id > 0)
			{
				try
				{
					await _resourceClient.Delete(AIResourceFormModel.Id);
					HttpContext.Session.SetString("Notification", $"The AI Resource was deleted successfully.");
					return RedirectToPage("Settings");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The AI Resource was not deleted successfully.");
					HttpContext.Session.SetString("Error", $"The AI Resource was not deleted successfully.");
					return RedirectToPage("Settings");
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The AI Resource was not deleted successfully.");
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostResultTypeSaveAsync(SettingsResultTypeModel ResultTypeFormModel)
		{
			if (ResultTypeFormModel != null && ResultTypeFormModel.IsValid())
			{
				ResultTypeModel item = new ResultTypeModel()
				{
					Id = ResultTypeFormModel.Id,
					Name = ResultTypeFormModel.Name,
					Description = ResultTypeFormModel.Description,
					Active = true
				};
				try
				{
					if (item.Id.Equals(0))
					{
						await _resultTypeClient.Create(item);
						HttpContext.Session.SetString("Notification", $"The Result Type was created successfully.");
					}
					else
					{
						await _resultTypeClient.Update(item);
						HttpContext.Session.SetString("Notification", $"The Result Type was updated successfully.");
					}
					return RedirectToPage("Settings");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The Result Type was not saved successfully.");
					HttpContext.Session.SetString("Error", $"The Result Type was not saved successfully.");
					return RedirectToPage("Settings");
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The Result Type was not saved successfully.");
				return BadRequest();
			}
		}

		public async Task<IActionResult> OnPostResultTypeDeleteAsync(SettingsResultTypeModel ResultTypeFormModel)
		{
			if (ResultTypeFormModel != null && ResultTypeFormModel.Id > 0)
			{
				try
				{
					await _resultTypeClient.Delete(ResultTypeFormModel.Id);
					HttpContext.Session.SetString("Notification", $"The Result Type was deleted successfully.");
					return RedirectToPage("Settings");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "The Result Type was not deleted successfully.");
					HttpContext.Session.SetString("Error", $"The Result Type was not deleted successfully.");
					return RedirectToPage("Settings");
				}
			}
			else
			{
				//log error and respond with a bad request
				HttpContext.Session.SetString("Error", $"The Result Type was not deleted successfully.");
				return BadRequest();
			}
		}

		private SelectList CreateSelectList<T>(IEnumerable<T> itemList, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
		{
			var options = new List<SelectListItem>();
			options.Add(new SelectListItem { Value = "", Text = "Select an item" });

			if (itemList != null)
			{
				options.AddRange(itemList.Select(item => new SelectListItem {
					Value = funcToGetValue(item).ToString(),
					Text = funcToGetText(item).ToString()
				}));
			}
			return new SelectList(options, "Value", "Text");
		}

	}
}
