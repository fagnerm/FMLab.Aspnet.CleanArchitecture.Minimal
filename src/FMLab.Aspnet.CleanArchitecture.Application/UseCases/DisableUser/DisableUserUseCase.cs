// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableUser;

public class DisableUserUseCase : TransactionalUseCaseBase<DisableUserInputDTO, DisableUserOutputDTO>, IDisableUserUseCase
{
    private readonly IUserRepository _repository;

    public DisableUserUseCase(IUnitOfWork unitOfWork, IUserRepository repository)
        : base(unitOfWork)
    {
        _repository = repository;
    }

    public override async Task<Result> ExecuteHandlerAsync(DisableUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(input.Id, cancellationToken);

        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        user.Deactivate();
        _repository.Update(user);

        return Result.Success();
    }
}
