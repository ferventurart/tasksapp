using FluentAssertions;
using NSubstitute;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Commands;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Persistence;

namespace TestProject1.Features.Todos.Commands;

public class CreateTodoCommandTests
{
    private static readonly CreateTodoCommand Command = new("This is a description", "02/18/2024", "Yellow");

    private readonly CreateTodoCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    public CreateTodoCommandTests()
    {
        var todoRepositoryMock = Substitute.For<ITodoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateTodoCommandHandler(
            todoRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenCategoryIsNotValid()
    {
        //Arrange
        CreateTodoCommand invalidCommand = Command with { Category = "invalid_category" };
        
        //Act
        Result<Guid> result = await _handler.Handle(invalidCommand, default);
        
        //Assert
        result.Error.Should().Be(TodoErrors.NotValidCategory("invalid_category"));
    }
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCreateSucceeds()
    {
        //Act
        Result<Guid> result = await _handler.Handle(Command, default);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenCreateSucceeds()
    {
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}