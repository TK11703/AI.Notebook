using FluentValidation;

namespace AI.Notebook.API.Validation;

public class ResultTypeModelValidator : AbstractValidator<ResultTypeModel>
{
	public ResultTypeModelValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.Description)
			.Length(0, 250);
	}
}
