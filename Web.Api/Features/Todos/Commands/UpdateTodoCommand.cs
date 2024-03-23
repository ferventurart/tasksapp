using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Commands;

public sealed record UpdateTodoCommand(
    Guid Id, 
    string Description, 
    string? DueDate,
    string Category, 
    string Status) : ICommand<Guid>;

internal sealed class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithErrorCode(TodoErrorCodes.UpdateTodo.MissingId);
        
        RuleFor(c => c.Description)
            .NotEmpty().WithErrorCode(TodoErrorCodes.CreateTodo.MissingDescription)
            .MaximumLength(1024).WithErrorCode(TodoErrorCodes.CreateTodo.DescriptionInvalidLength);

        RuleFor(c => c.DueDate)
            .Matches("^((0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])/[0-9]{4})$")
            .WithErrorCode(TodoErrorCodes.CreateTodo.InvalidDueDate);

        RuleFor(c => c.Status)
            .NotEmpty().WithErrorCode(TodoErrorCodes.UpdateTodo.MissingStatus);

        RuleFor(c => c.Category)
            .NotEmpty().WithErrorCode(TodoErrorCodes.CreateTodo.MissingCategory);
    }
}

public sealed class UpdateTodoCommandHandler(AppDbContext dbContext) : ICommandHandler<UpdateTodoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        
        if (todo is null)
        {
            return Result.Failure<Guid>(TodoErrors.NotFound(request.Id));
        }
        
        if (TodoStatus.FromName(request.Status) is not { } status)
        {
            return Result.Failure<Guid>(TodoErrors.NotValidStatus(request.Status));
        }
        
        if (TodoCategory.FromName(request.Category) is not { } category)
        {
            return Result.Failure<Guid>(TodoErrors.NotValidCategory(request.Category));
        }
        
        todo.Description = request.Description;
        todo.DueDate = request.DueDate;
        todo.Category = category;
        todo.Status = status;

        dbContext.Todos.Update(todo);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return todo.Id;
    }
}