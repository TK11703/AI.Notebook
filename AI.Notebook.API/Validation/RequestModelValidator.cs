using FluentValidation;

namespace AI.Notebook.API.Validation;

public class RequestModelValidator : AbstractValidator<RequestModel>
{
	public RequestModelValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}
