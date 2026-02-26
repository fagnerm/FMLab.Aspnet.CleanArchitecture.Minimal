// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.CreateUser;

public class CreateUserUseCase : TransactionalUseCaseBase<CreateUserInputDTO, CreateUserOutputDTO>, ICreateUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUserGateway _gateway;
    private readonly IValidator<CreateUserInputDTO> _validator;

    public CreateUserUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserGateway gateway, IValidator<CreateUserInputDTO> validator)
        : base(unitOfWork)
    {
        _repository = userRepository;
        _validator = validator;
        _gateway = gateway;
    }

    public override async Task<Result> ExecuteHandlerAsync(CreateUserInputDTO input, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(input);
        if (!validation.IsValid)
            return Result.Validation(error: validation.Errors[0].ErrorMessage);

        var name = new Name(input.Name);
        var email = input.Email is null ? null : new Email(input.Email);

        var found = await _gateway.ExistsByKeyAsync(name.Value, email?.Value, cancellationToken);

        if (found) return Result.Conflict("User already exists");

        var user = new User(name, email);
        await _repository.AddAsync(user, cancellationToken);

        var result = new CreateUserOutputDTO(user.Id, user.Name.Value, user.Email?.Value, user.Status.ToString());

        return Result.Success(result);
    }
}
