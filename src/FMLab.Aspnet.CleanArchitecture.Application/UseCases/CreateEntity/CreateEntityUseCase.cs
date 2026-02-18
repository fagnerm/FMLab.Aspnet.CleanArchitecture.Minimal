// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class CreateEntityUseCase : ICreateEntityUseCase
{
    private readonly IEntityRepository _categoryRepository;

    public CreateEntityUseCase(IEntityRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<UseCaseResult<CreateEntityOutputDTO>> ExecuteAsync(CreateEntityInputDTO input, CancellationToken cancellationToken)
    {
        var name = new Name(input.Name);
        var category = new Entity(name);

        var found = await _categoryRepository.ExistsAsync(category);

        if (found)
        {
            return UseCaseResult<CreateEntityOutputDTO>.Failure("Category already exists");
        }


        await _categoryRepository.AddAsync(category);

        return UseCaseResult<CreateEntityOutputDTO>.Success();
    }
}
