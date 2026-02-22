// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.DTOs;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.UseCases.Shared;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.Shared;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases;

public class GetUserUseCase : IUseCase<GetUserInputDTO, UserSummaryDTO>, IGetUserUseCase
{
    private readonly IUserGateway _gateway;

    public GetUserUseCase(IUserGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<UseCaseResult<UserSummaryDTO>> ExecuteAsync(GetUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _gateway.ListUserByIdAsync(input.Id, cancellationToken);

        if (user == null)
        {
            return UseCaseResult<UserSummaryDTO>.Failure(error: "User not found");
        }

        return UseCaseResult<UserSummaryDTO>.Success(user);
    }
}
