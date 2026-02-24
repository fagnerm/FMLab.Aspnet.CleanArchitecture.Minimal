// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;

public class PatchUserUseCase : TransactionalUseCaseBase<UpdateUserInputDTO, UpdateUserOutputDTO>, IPatchUserUseCase
{
    private readonly IUserRepository _repository;

    public PatchUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<Result<UpdateUserOutputDTO>> ExecuteHandlerAsync(UpdateUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(input.Id, cancellationToken);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var name = input.Name is null ? user.Name : new Name(input.Name);
        var email = input.Email is null ? user.Email : new Email(input.Email);
        user.Update(name, email);

        _repository.Update(user);

        var result = new UpdateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value);

        return Result<UpdateUserOutputDTO>.Success(result);
    }
}
