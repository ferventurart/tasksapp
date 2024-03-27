using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Features.Todos.Errors;
using Web.Api.Features.Todos.Persistence;

namespace Web.Api.Features.Todos.Queries;

public record GetTodoByIdQuery(Guid Id) : IQuery<TodoResponse>;

internal sealed class GetTodoByIdQueryHandler(ITodoRepository todoRepository) : IQueryHandler<GetTodoByIdQuery, TodoResponse>
{
    public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        return todo?.ToResponse() ?? Result.Failure<TodoResponse>(TodoErrors.NotFound(request.Id));
    }
}