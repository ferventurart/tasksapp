using System.ComponentModel.DataAnnotations;

namespace Web.Api.Features.Todos.Models;

public sealed class Todo
{
    public Guid Id { get; init; }
    [MaxLength(1024)] 
    public string Description { get; set; } = string.Empty;
    public TodoStatus Status { get; set; } = null!;
    [MaxLength(10)]
    public string? DueDate { get; set ; } = string.Empty;
    public TodoCategory Category { get; set; } = null!;
}