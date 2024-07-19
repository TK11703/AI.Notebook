using AI.Notebook.Common.Entities;
using FluentValidation;

namespace AI.Notebook.API.Validation;

public class ResultModelValidator : AbstractValidator<ResultBase>
{
	public ResultModelValidator()
	{
		RuleFor(x => x.RequestId)
			.NotEmpty()
			.GreaterThan(0);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

		RuleFor(x => x.ResultTypeId)
			.NotEmpty()
			.GreaterThan(0);
	}
}
