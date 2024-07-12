﻿using FluentValidation;

namespace AI.Notebook.API.Validation;

public class RequestSpeechValidator : AbstractValidator<RequestSpeechModel>
{
	public RequestSpeechValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}