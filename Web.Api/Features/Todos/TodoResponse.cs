namespace Web.Api.Features.Todos;

public record TodoResponse(
    Guid Id,
    string Description,
    string Status,
    string DueDate,
    string Category);