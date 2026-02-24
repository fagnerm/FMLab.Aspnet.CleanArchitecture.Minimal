// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FluentValidation;
using FluentValidation.Results;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces;
using FMLab.Aspnet.CleanArchitecture.Application.Interfaces.Repositories;
using FMLab.Aspnet.CleanArchitecture.Application.Shared.Result;
using FMLab.Aspnet.CleanArchitecture.Application.UseCases.UpdateUser;
using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;
using NSubstitute;

namespace FMLab.Aspnet.CleanArchitecture.Tests.Application.UseCases.UpdateUser;

public class UpdateUserUseCaseTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IUserRepository _repository = Substitute.For<IUserRepository>();
    private readonly IValidator<UpdateUserInputDTO> _validator = Substitute.For<IValidator<UpdateUserInputDTO>>();
    private readonly UpdateUserUseCase _useCase;

    public UpdateUserUseCaseTests()
    {
        _useCase = new UpdateUserUseCase(_unitOfWork, _repository, _validator);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ReturnsSuccess()
    {
        var user = new User(new Name("Fagner"), null);
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult());
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", null), CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_WhenSuccessful_CommitsUnitOfWork()
    {
        var user = new User(new Name("Fagner"), null);
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult());
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", null), CancellationToken.None);

        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenSuccessful_CallsRepositoryUpdate()
    {
        var user = new User(new Name("Fagner"), null);
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult());
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, "John", null), CancellationToken.None);

        _repository.Received(1).Update(user);
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidationFails_ReturnsValidationResult()
    {
        var failure = new ValidationFailure("Name", "Name is required");
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult(new[] { failure }));

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, null, null), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultType.Validation, result.Type);
        Assert.Equal("Name is required", result.Error);
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidationFails_DoesNotQueryRepository()
    {
        var failure = new ValidationFailure("Name", "Name is required");
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult(new[] { failure }));

        await _useCase.ExecuteAsync(new UpdateUserInputDTO(1, null, null), CancellationToken.None);

        await _repository.DidNotReceive().GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ReturnsNotFound()
    {
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult());
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        var result = await _useCase.ExecuteAsync(new UpdateUserInputDTO(99, "John", null), CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultType.NotFound, result.Type);
        Assert.Equal("User not found", result.Error);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_DoesNotCommit()
    {
        _validator.Validate(Arg.Any<UpdateUserInputDTO>()).Returns(new ValidationResult());
        _repository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        await _useCase.ExecuteAsync(new UpdateUserInputDTO(99, "John", null), CancellationToken.None);

        await _unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}
