// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;

public interface IUpdateUserUseCase : IUseCase<UpdateUserInputDTO, UpdateUserOutputDTO>
{
}
