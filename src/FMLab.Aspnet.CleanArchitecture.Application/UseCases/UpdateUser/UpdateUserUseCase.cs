// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;

public class UpdateUserUseCase : TransactionalUseCaseBase<UpdateUserInputDTO, UpdateUserOutputDTO>, IUpdateUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UpdateUserInputDTO> _validator;

    public UpdateUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository, IValidator<UpdateUserInputDTO> validator)
        : base(unitOfWork)
    {
        _repository = repository;
        _validator = validator;
    }

    public override async Task<Result<UpdateUserOutputDTO>> ExecuteHandlerAsync(UpdateUserInputDTO input, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(input);
        if (!validation.IsValid)
            return Result<UpdateUserOutputDTO>.Validation(error: validation.Errors[0].ErrorMessage);

        var user = await _repository.GetByIdAsync(input.Id, cancellationToken);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? null : new Email(input.Email!);
        user.Update(name, email);

        _repository.Update(user);

        return Result<UpdateUserOutputDTO>.Success();
    }
}
