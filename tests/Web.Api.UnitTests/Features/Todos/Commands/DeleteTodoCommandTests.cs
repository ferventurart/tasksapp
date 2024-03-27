using FluentAssertions;
using NSubstitute;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Commands;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Models;
using Web.Api.Features.Todos.Persistence;

namespace TestProject1.Features.Todos.Commands;

public class DeleteTodoCommandTests
{
    private static readonly DeleteTodoCommand Command = new(Guid.NewGuid());

    private static readonly Todo Todo = new ()
    {
        Description = "Toro Vince",
        Category = TodoCategory.Blue,
        DueDate = "02/02/2023",
        Status = TodoStatus.Completed
    };
    private readonly DeleteTodoCommandHandler _handler;
    private readonly ITodoRepository _todoRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public DeleteTodoCommandTests()
    {
        _todoRepositoryMock = Substitute.For<ITodoRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new DeleteTodoCommandHandler(
            _todoRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenTodoNotExists()
    {
        //Arrange
        DeleteTodoCommand invalidCommand = Command with { Id = Guid.Empty };
        //Act
        Result<Guid> result = await _handler.Handle(invalidCommand, default);
        //Assert
        result.Error.Should().Be(TodoErrors.NotFound(Guid.Empty));
    }
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCreateSucceeds()
    {
        //Arrange
        _todoRepositoryMock.GetByIdAsync(Command.Id, default)
            .Returns(Todo);
        //Act
        Result<Guid> result = await _handler.Handle(Command, default);
        //Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenDeleteSucceeds()
    {
        //Arrange
        _todoRepositoryMock.GetByIdAsync(Command.Id, default)
            .Returns(Todo);
        
        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}