using FluentValidation;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Commands;

public sealed record CreateTodoCommand(
    string Description, 
    string? DueDate, 
    string Category) : ICommand<Guid>;

internal sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithErrorCode(TodoErrorCodes.CreateTodo.MissingDescription)
            .MaximumLength(1024).WithErrorCode(TodoErrorCodes.CreateTodo.DescriptionInvalidLength);

        RuleFor(c => c.DueDate)
            .Matches("^((0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])/[0-9]{4})$")
            .WithErrorCode(TodoErrorCodes.CreateTodo.InvalidDueDate);

        RuleFor(c => c.Category)
            .NotEmpty().WithErrorCode(TodoErrorCodes.CreateTodo.MissingCategory);
    }
}

public sealed class CreateTodoCommandHandler(AppDbContext dbContext) : ICommandHandler<CreateTodoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        if (TodoCategory.FromName(request.Category) is not { } category)
        {
            return Result.Failure<Guid>(TodoErrors.NotValidCategory(request.Category));
        }
        
        var todo = new Todo()
        {
            Description = request.Description,
            DueDate = request.DueDate,
            Category = category,
            Status = TodoStatus.Pending
        };

        await dbContext.Todos.AddAsync(todo, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return todo.Id;
    }
}
