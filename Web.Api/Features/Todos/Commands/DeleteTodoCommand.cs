using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Errors;

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

public sealed class DeleteTodoCommandHandler(AppDbContext dbContext) : ICommandHandler<DeleteTodoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (todo is null)
        {
            return Result.Failure<Guid>(TodoErrors.NotFound(request.Id));
        }

        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return todo.Id;
    }
}