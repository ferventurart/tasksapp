using Microsoft.Extensions.Caching.Memory;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Persistence;

public class CachedTodoRepository(
    TodoRepository decorated, 
    IMemoryCache memoryCache) : ITodoRepository
{
    public async Task<IList<Todo>> GetAllAsync(string status, string category, CancellationToken cancellationToken)
    {
        var key = $"get-todos-{status}-{category}";

        return (await memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return decorated.GetAllAsync(status, category, cancellationToken);
            }))!;
    }

    public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var key = $"todo-{id}";

        return await memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return decorated.GetByIdAsync(id, cancellationToken);
            });
    }

    public void Add(Todo todo)
    {
        decorated.Add(todo);
    }

    public void Update(Todo todo)
    {
        decorated.Update(todo);
    }

    public void Delete(Todo todo)
    {
        decorated.Delete(todo);
    }
}