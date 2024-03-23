using Web.Api.Common.Models;

namespace Web.Api.Features.Todos.Models;

public sealed class TodoStatus : Enumeration<TodoStatus>
{
    public static readonly TodoStatus Pending = new (1, "Pending");
    public static readonly TodoStatus Cancelled = new (2, "Cancelled");
    public static readonly TodoStatus Completed = new (3, "Completed");
    private TodoStatus(int value, string name) : base(value, name)
    {
    }
}