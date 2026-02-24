// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.DisableUser;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;
using NSubstitute;

namespace FMLab.Aspnet.CleanArchitecture.Tests.Application.UseCases.DisableUser;

public class DisableUserUseCaseTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
    private readonly DisableUserUseCase _useCase;

    public DisableUserUseCaseTests()
    {
        _useCase = new DisableUserUseCase(_unitOfWork, _repository);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserActive_ReturnsSuccess()
    {
        var user = new User(new Name("Fagner"), null);
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new DisableUserInputDTO(1), CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserActive_SetsStatusToDeactivated()
    {
        var user = new User(new Name("Fagner"), null);
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        await _useCase.ExecuteAsync(new DisableUserInputDTO(1), CancellationToken.None);

        Assert.Equal(UserStatus.Deactivated, user.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WhenSuccessful_CommitsUnitOfWork()
    {
        var user = new User(new Name("Fagner"), null);
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        await _useCase.ExecuteAsync(new DisableUserInputDTO(1), CancellationToken.None);

        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ReturnsNotFound()
    {
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        var result = await _useCase.ExecuteAsync(new DisableUserInputDTO(99), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultType.NotFound, result.Type);
        Assert.Equal("User not found", result.Error);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_DoesNotCommit()
    {
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        await _useCase.ExecuteAsync(new DisableUserInputDTO(99), CancellationToken.None);

        await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserAlreadyDeactivated_ReturnsDomainError()
    {
        var user = new User(new Name("Fagner"), null);
        user.Deactivate();
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new DisableUserInputDTO(1), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultType.Domain, result.Type);
        Assert.Equal("User already deactivated", result.Error);
    }

    [Fact]
    public async Task ExecuteAsync_WhenDomainExceptionThrown_DoesNotCommit()
    {
        var user = new User(new Name("Fagner"), null);
        user.Deactivate();
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        await _useCase.ExecuteAsync(new DisableUserInputDTO(1), CancellationToken.None);

        await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}
