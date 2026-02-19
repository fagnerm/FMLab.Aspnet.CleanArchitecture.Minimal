// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Users;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class CreateUserUseCase : TransactionalUseCaseBase<CreateUserInputDTO, CreateUserOutputDTO>, ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;

    public CreateUserUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository)
        : base(unitOfWork)
    {
        _userRepository = userRepository;
    }

    public override async Task<UseCaseResult<CreateUserOutputDTO>> ExecuteHandlerAsync(CreateUserInputDTO input, CancellationToken cancellationToken)
    {
        var name = new Name(input.Name);
        var user = new User(name);

        var found = await _userRepository.ExistsAsync(user);

        if (found)
        {
            return UseCaseResult<CreateUserOutputDTO>.Failure("User already exists");
        }

        await _userRepository.AddAsync(user);

        return UseCaseResult<CreateUserOutputDTO>.Success();
    }
}
