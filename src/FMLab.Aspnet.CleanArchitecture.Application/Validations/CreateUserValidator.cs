// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.Validations;

internal class CreateUserValidator : AbstractValidator<CreateUserInputDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}
