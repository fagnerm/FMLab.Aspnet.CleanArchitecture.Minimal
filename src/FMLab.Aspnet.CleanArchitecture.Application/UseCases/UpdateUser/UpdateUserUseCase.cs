// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Users;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class UpdateUserUseCase : TransactionalUseCaseBase<UpdateUserInputDTO, UpdateUserOutputDTO>, IUpdateUserUseCase
{
    protected readonly IUserRepository _repository;

    public UpdateUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<UseCaseResult<UpdateUserOutputDTO>> ExecuteHandlerAsync(UpdateUserInputDTO input, CancellationToken token)
    {
        var user = await _repository.GetByIdAsync(input.Id, token);

        if (user is null) UseCaseResult<UpdateUserOutputDTO>.Failure(error: "User not found");

        MapUserUpdate(input, ref user!);

        _repository.Update(user);

        return UseCaseResult<UpdateUserOutputDTO>.Success();
    }

    protected virtual void MapUserUpdate(UpdateUserInputDTO input, ref User user)
    {
        var name = new Name(input.Name!);
        var email = new Email(input.Email!);

        user.Update(name, email);
        _repository.Update(user);
    }
}
