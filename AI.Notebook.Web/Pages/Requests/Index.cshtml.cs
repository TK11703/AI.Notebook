using AI.Notebook.Common.Models;
using AI.Notebook.Web.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI.Notebook.Web.Pages.Requests
{
    public class IndexModel : PageModel
    {
		private readonly RequestClient _requestClient;
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, RequestClient requestClient)
		{
			_logger = logger;
			_requestClient = requestClient;
			PageResult = new PageResultModel<RequestModel>(CurrentPage, 0);
			PageSubmission = new PageSubmissionModel();
		}

		[BindProperty(SupportsGet = true)]
		public int CurrentPage { get; set; } = 1;

		[BindProperty]
		public PageSubmissionModel PageSubmission { get; set; }

		[BindProperty]
		public PageResultModel<RequestModel> PageResult { get; set; }

        public async Task OnGetAsync()
        {
			PageSubmission.SetDefaults(CurrentPage, 10, "Name", "Asc");
			try
			{
				PageResult = await _requestClient.GetAll(PageSubmission);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the application list.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResultModel<RequestModel>(PageSubmission.PageSize, PageSubmission.Start);
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
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while requesting the application list.");
				HttpContext.Session.SetString("Error", "The page request could not be completed. Please contact support if this continues to occur.");
			}
			if (PageResult == null)
			{
				PageResult = new PageResultModel<RequestModel>(PageSubmission.PageSize, PageSubmission.Start);
			}
			return Page();
		}
	}
}
