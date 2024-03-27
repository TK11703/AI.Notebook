using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AI.Notebook.Web.Pages.Results
{
    public class ItemModel : PageModel
    {
		[FromRoute]
		public int Id { get; set; }

		public void OnGet()
        {
        }
    }
}
