// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Gateways;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;

namespace FMLab.Aspnet.CleanArchitecture.Application.UseCases.GetUser;

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserGateway _gateway;

    public GetUserUseCase(IUserGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<Result> ExecuteAsync(GetUserInputDTO input, CancellationToken cancellationToken)
    {
        var user = await _gateway.ListUserByIdAsync(input.Id, cancellationToken);

        if (user == null)
        {
            return Result.NotFound("User not found");
        }

        return Result.Success(user);
    }
}
