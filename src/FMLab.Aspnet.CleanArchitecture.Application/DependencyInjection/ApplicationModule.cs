// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.CreateUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DeleteUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.GetUser;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.ListUsers;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;
using Microsoft.Extensions.DependencyInjection;

namespace FMLab.Aspnet.CleanArchitecture.Application.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserInputDTO>, CreateUserValidator>();
        services.AddScoped<IValidator<UpdateUserInputDTO>, UpdateUserValidator>();

        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IDisableUserUseCase, DisableUserUseCase>();

        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IPatchUserUseCase, PatchUserUseCase>();

        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();


        services.AddScoped<IListUsersUseCase, ListUsersUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();

        return services;
    }
}
