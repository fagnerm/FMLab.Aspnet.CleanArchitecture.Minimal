// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;

public class UpdateUserUseCase : TransactionalUseCaseBase<UpdateUserInputDTO, UpdateUserOutputDTO>, IUpdateUserUseCase
{
    protected readonly IUserRepository _repository;

    public UpdateUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<Result<UpdateUserOutputDTO>> ExecuteHandlerAsync(UpdateUserInputDTO input, CancellationToken token)
    {
        var user = await _repository.GetByIdAsync(input.Id, token);

        if (user is null) return Result<UpdateUserOutputDTO>.NotFound("User not found");

        var (name, email) = MapUserUpdate(input, ref user!);
        user.Update(name, email);

        _repository.Update(user);

        return Result<UpdateUserOutputDTO>.Success();
    }

    protected virtual Tuple<Name, Email?> MapUserUpdate(UpdateUserInputDTO input, ref User user)
    {
        var name = new Name(input.Name!);
        var email = string.IsNullOrEmpty(input.Email) ? null : new Email(input.Email!);

        return Tuple.Create(name, email);
    }
}
