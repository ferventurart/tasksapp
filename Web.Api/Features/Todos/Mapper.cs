using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos;

public static class Mapper
{
    public static TodoResponse ToResponse(this Todo todo)
    {
        return new TodoResponse(todo.Id,
            todo.Description,
            todo.Status.Name,
            todo.DueDate ?? string.Empty,
            todo.Category.Name);
    }
}