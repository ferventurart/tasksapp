using Microsoft.EntityFrameworkCore;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Errors;

namespace Web.Api.Features.Todos.Queries;

public record GetTodoByIdQuery(Guid Id) : IQuery<TodoResponse>;

public sealed class GetTodoByIdQueryHandler(AppDbContext dbContext) : IQueryHandler<GetTodoByIdQuery, TodoResponse>
{
    public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await dbContext.Todos.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        return todo?.ToResponse() ?? Result.Failure<TodoResponse>(TodoErrors.NotFound(request.Id));
    }
}