using System.ComponentModel.DataAnnotations;

namespace AI.Notebook.Common.Models;

public class PageSubmissionModel
{
	public int Start { get; set; } = 0;

	[Range(10, 100, ErrorMessage = "Page Size invalid (10-100).")]
	public int PageSize { get; set; }

	public DateTime? BeginDate { get; set; }

	public DateTime? EndDate { get; set; }

	public string? Filter { get; set; } = string.Empty;

	[Required]
	public string? SortBy { get; set; }

	[Required]
	public string? SortDirection { get; set; }

	[Display(Name = "Date Range")]
	public string? DateRange
	{
		get
		{
			if (BeginDate.HasValue && EndDate.HasValue)
			{
				return $"{BeginDate.Value.ToString("MM/dd/yyyy")} - {EndDate.Value.ToString("MM/dd/yyyy")}";
			}
			return string.Empty;
		}
	}

	public PageSubmissionModel()
	{

	}

	public bool ContainsFilter()
	{
		if(!string.IsNullOrEmpty(Filter) || (BeginDate.HasValue && EndDate.HasValue))
			return true;
		return false;
	}

	public void SetDefaults(int page, int pageSize, string sortBy, string sortDirection)
	{
		SetPage(page);
		PageSize = pageSize;
		SortBy = sortBy;
		SortDirection = sortDirection;
	}

	public void SetPage(int page)
	{
		Start = (PageSize * page) - PageSize;
	}
}