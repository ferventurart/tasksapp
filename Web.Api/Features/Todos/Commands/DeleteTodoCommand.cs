using FluentValidation;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Persistence;

namespace Web.Api.Features.Todos.Commands;

public sealed record DeleteTodoCommand(Guid Id) : ICommand<Guid>;

internal sealed class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(TodoErrorCodes.UpdateTodo.MissingId);
    }
}

internal sealed class DeleteTodoCommandHandler(
    ITodoRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteTodoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        if (todo is null)
        {
            return Result.Failure<Guid>(TodoErrors.NotFound(request.Id));
        }

        repository.Delete(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}