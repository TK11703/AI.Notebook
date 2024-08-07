﻿using AI.Notebook.Common.Entities;
using FluentValidation;

namespace AI.Notebook.API.Validation;

public class RequestBaseValidator : AbstractValidator<RequestBase>
{
	public RequestBaseValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(2, 150);

		RuleFor(x => x.ResourceId)
			.NotEmpty()
			.GreaterThan(0);

	}
}
