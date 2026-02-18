// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableEntity;

public class DisableEntityUseCase : IDisableEntityUseCase
{
    private IEntityRepository _repository;

    public DisableEntityUseCase(IEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<DisableEntityOutputDTO>> ExecuteAsync(DisableEntityInputDTO input, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(input.Id);

        if (entity == null)
        {
            return UseCaseResult<DisableEntityOutputDTO>.Failure("Entity not found");
        }

        entity.Disable();
        await _repository.UpdateAsync(entity);

        return UseCaseResult<DisableEntityOutputDTO>.Success();
    }
}
