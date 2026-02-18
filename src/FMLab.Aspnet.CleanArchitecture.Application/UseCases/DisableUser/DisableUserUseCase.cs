// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class DisableUserUseCase : IDisableUserUseCase
{
    private IUserRepository _repository;

    public DisableUserUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<DisableUserOutputDTO>> ExecuteAsync(DisableUserInputDTO input, CancellationToken cancellationToken)
    {
        var User = await _repository.GetByIdAsync(input.Id);

        if (User == null)
        {
            return UseCaseResult<DisableUserOutputDTO>.Failure("User not found");
        }

        User.Disable();
        await _repository.UpdateAsync(User);

        return UseCaseResult<DisableUserOutputDTO>.Success();
    }
}
