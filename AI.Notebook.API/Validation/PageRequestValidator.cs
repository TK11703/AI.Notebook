using AI.Notebook.Common.Entities;
using FluentValidation;

namespace AI.Notebook.API.Validation;

public class PageRequestValidator : AbstractValidator<PageRequest>
{
	public PageRequestValidator()
	{
		RuleFor(x => x.SortBy)
			.NotEmpty();

		RuleFor(x => x.SortDirection)
			.NotEmpty();

		RuleFor(x => x.PageSize)
			.NotEmpty()
			.GreaterThanOrEqualTo(10)
			.LessThanOrEqualTo(100);

	}
}
