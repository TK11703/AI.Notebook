using FluentValidation;

namespace AI.Notebook.API.Validation;

public class RequestVisionValidator : AbstractValidator<RequestVisionModel>
{
	public RequestVisionValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}
