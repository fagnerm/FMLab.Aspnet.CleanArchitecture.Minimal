// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.ResultTypes;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;
using NSubstitute;

namespace FMLab.Aspnet.CleanArchitecture.Tests.Application.UseCases.UpdateUser;

public class PatchUserUseCaseTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
    private readonly PatchUserUseCase _useCase;

    public PatchUserUseCaseTests()
    {
        _useCase = new PatchUserUseCase(_unitOfWork, _repository);
    }

    [Fact]
    public async Task ExecuteAsync_WithNewName_UpdatesOnlyName()
    {
        var user = new User(new Name("Fagner"), new Email("fagner@example.com"));
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", null), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("John", result.Data<UpdateUserOutputDTO>().Name);
        Assert.Equal("fagner@example.com", result.Data<UpdateUserOutputDTO>().Email);
    }

    [Fact]
    public async Task ExecuteAsync_WithNewEmail_UpdatesOnlyEmail()
    {
        var user = new User(new Name("Fagner"), new Email("fagner@example.com"));
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, null, "new@example.com"), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Fagner", result.Data<UpdateUserOutputDTO>()!.Name);
        Assert.Equal("new@example.com", result.Data<UpdateUserOutputDTO>().Email);
    }

    [Fact]
    public async Task ExecuteAsync_WithBothFields_UpdatesBoth()
    {
        var user = new User(new Name("Fagner"), new Email("fagner@example.com"));
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", "john@example.com"), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("John", result.Data<UpdateUserOutputDTO>()!.Name);
        Assert.Equal("john@example.com", result.Data<UpdateUserOutputDTO>().Email);
    }

    [Fact]
    public async Task ExecuteAsync_WithNoFields_PreservesBoth()
    {
        var user = new User(new Name("Fagner"), new Email("fagner@example.com"));
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, null, null), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Fagner", result.Data<UpdateUserOutputDTO>()!.Name);
        Assert.Equal("fagner@example.com", result.Data<UpdateUserOutputDTO>().Email);
    }

    [Fact]
    public async Task ExecuteAsync_WhenSuccessful_ReturnsPopulatedOutputDTO()
    {
        var user = new User(new Name("Fagner"), null);
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", null), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data<UpdateUserOutputDTO>());
        Assert.Equal("John", result.Data<UpdateUserOutputDTO>().Name);
        Assert.Null(result.Data<UpdateUserOutputDTO>().Email);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ReturnsNotFound()
    {
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(99, "John", null), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultType.NotFound, result.Type);
        Assert.Equal("User not found", result.Error);
    }
}
