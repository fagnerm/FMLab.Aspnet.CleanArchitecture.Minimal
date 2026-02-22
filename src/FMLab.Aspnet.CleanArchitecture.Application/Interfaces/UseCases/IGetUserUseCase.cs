// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;

public interface IGetUserUseCase : IUseCase<GetUserInputDTO, UserSummaryDTO>
{
}
