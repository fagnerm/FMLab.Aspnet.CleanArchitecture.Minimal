// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;

public class PatchUserUseCase : UpdateUserUseCase, IPatchUserUseCase
{
    public PatchUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository) : base(unitOfWork, repository)
    {
    }

    protected override Tuple<Name, Email?> MapUserUpdate(UpdateUserInputDTO input, ref User user)
    {
        var name = input.Name is null ? user.Name : new Name(input.Name);
        var email = input.Email is null ? user.Email : new Email(input.Email);

        return Tuple.Create(name, email);
    }
}
