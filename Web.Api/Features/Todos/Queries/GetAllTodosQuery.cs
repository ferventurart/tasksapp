using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Queries;

public sealed record GetAllTodosQuery(
    string Status,
    string Category) : IQuery<IReadOnlyList<TodoResponse>>;

public sealed class GetAllTodosQueryHandler(AppDbContext dbContext)
    : IQueryHandler<GetAllTodosQuery, IReadOnlyList<TodoResponse>>
{
    public async Task<Result<IReadOnlyList<TodoResponse>>> Handle(GetAllTodosQuery request,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Todos.AsQueryable();

        if (TodoStatus.FromName(request.Status) is { } status)
            query = query.Where(w => w.Status == status);

        if (TodoCategory.FromName(request.Category) is { } category)
            query = query.Where(w => w.Category == category);

        var results = await query.ToListAsync(cancellationToken);

        return results.Select(s => s.ToResponse()).ToImmutableList();
    }
}