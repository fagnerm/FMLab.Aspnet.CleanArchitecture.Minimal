// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.CreateUser;

internal class CreateUserValidator : AbstractValidator<CreateUserInputDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}
