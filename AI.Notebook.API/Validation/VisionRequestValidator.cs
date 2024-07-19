using AI.Notebook.Common.Entities;
using FluentValidation;

namespace AI.Notebook.API.Validation;

public class VisionRequestValidator : AbstractValidator<VisionRequest>
{
	public VisionRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}
