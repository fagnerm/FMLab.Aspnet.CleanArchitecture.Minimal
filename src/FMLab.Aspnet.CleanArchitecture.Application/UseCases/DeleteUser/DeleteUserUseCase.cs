// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.DeleteUser;

public class DeleteUserUseCase : TransactionalUseCaseBase<DeleteUserInputDTO, DeleteUserOutputDTO>, IDeleteUserUseCase
{
    private readonly IUserRepository _repository;

    public DeleteUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<UseCaseResult<DeleteUserOutputDTO>> ExecuteHandlerAsync(DeleteUserInputDTO input, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(input.Id, cancellationToken);

        if (existingUser is null) UseCaseResult<DeleteUserOutputDTO>.Failure(error: "User already exists");

        _repository.Delete(existingUser!);

        return UseCaseResult<DeleteUserOutputDTO>.Success(message: "User deleted");
    }
}
