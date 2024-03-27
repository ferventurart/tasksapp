using System.Collections.Immutable;
using Web.Api.Common.Abstractions.Messaging;
using Web.Api.Common.Models;
using Web.Api.Features.Todos.Persistence;

namespace Web.Api.Features.Todos.Queries;

public sealed record GetAllTodosQuery(
    string Status,
    string Category) : IQuery<IReadOnlyList<TodoResponse>>;

public sealed class GetAllTodosQueryHandler(ITodoRepository repository)
    : IQueryHandler<GetAllTodosQuery, IReadOnlyList<TodoResponse>>
{
    public async Task<Result<IReadOnlyList<TodoResponse>>> Handle(
        GetAllTodosQuery request,
        CancellationToken cancellationToken)
    {
        var results = await repository.GetAllAsync(request.Status, request.Category, cancellationToken);
        return results.Select(s => s.ToResponse()).ToImmutableList();
    }
}