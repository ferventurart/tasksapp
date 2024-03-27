using Microsoft.EntityFrameworkCore;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Persistence;

public interface ITodoRepository
{
    Task<IList<Todo>> GetAllAsync(
        string status,
        string category,
        CancellationToken cancellationToken);
    Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(Todo todo);
    void Update(Todo todo);
    void Delete(Todo todo);
}

internal class TodoRepository(AppDbContext dbContext) : ITodoRepository
{
    public async Task<IList<Todo>> GetAllAsync(string status, string category, CancellationToken cancellationToken)
    {
        var query = dbContext.Todos.AsQueryable();

        if (TodoStatus.FromName(status) is { } todoStatus)
            query = query.Where(w => w.Status == todoStatus);

        if (TodoCategory.FromName(category) is { } todoCategory)
            query = query.Where(w => w.Category == todoCategory);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(f => 
            f.Id == id, 
            cancellationToken).ConfigureAwait(false);
    }

    public void Add(Todo todo)
    {
        dbContext.Add(todo);
    }

    public void Update(Todo todo)
    {
        dbContext.Update(todo);
    }

    public void Delete(Todo todo)
    {
        dbContext.Remove(todo);
    }
}