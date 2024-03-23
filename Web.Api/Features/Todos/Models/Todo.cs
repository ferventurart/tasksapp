using System.ComponentModel.DataAnnotations;

namespace Web.Api.Features.Todos.Models;

public class Todo
{
    public Guid Id { get; init; }
    [MaxLength(1024)]
    public string Description { get; set; } = string.Empty;
    public TodoStatus Status { get; set; }
    [MaxLength(10)]
    public string? DueDate { get; set; } = string.Empty;
    public TodoCategory Category { get; set; }
    
}