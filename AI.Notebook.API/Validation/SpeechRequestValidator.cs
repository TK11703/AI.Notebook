using AI.Notebook.Common.Entities;
using FluentValidation;

namespace AI.Notebook.API.Validation;

public class SpeechRequestValidator : AbstractValidator<SpeechRequest>
{
	public SpeechRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}
