// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
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
    private readonly IValidator<CreateUserInputDTO> _validator;

    public CreateUserUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository, IValidator<CreateUserInputDTO> validator)
        : base(unitOfWork)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public override async Task<UseCaseResult<CreateUserOutputDTO>> ExecuteHandlerAsync(CreateUserInputDTO input, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(input);
        if (!validation.IsValid)
            return UseCaseResult<CreateUserOutputDTO>.Failure(error: validation.Errors[0].ErrorMessage);

        var name = new Name(input.Name);
        var email = input.Email is null ? null : new Email(input.Email);

        var found = await _userRepository.ExistsAsync(name, cancellationToken);

        if (found)
        {
            return UseCaseResult<CreateUserOutputDTO>.Failure(error: "User already exists");
        }

        var user = new User(name, email);
        await _userRepository.AddAsync(user, cancellationToken);

        var result = new CreateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());

        return UseCaseResult<CreateUserOutputDTO>.Success(data: result, "User created");
    }
}
