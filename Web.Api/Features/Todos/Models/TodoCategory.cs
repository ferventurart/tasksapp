using Web.Api.Common.Models;

namespace Web.Api.Features.Todos.Models;

public sealed class TodoCategory : Enumeration<TodoCategory>
{
    public static readonly TodoCategory Orange = new (1, "Orange");
    public static readonly TodoCategory Yellow = new (2, "Yellow");
    public static readonly TodoCategory Blue = new (3, "Blue");
    private TodoCategory(int value, string name) : base(value, name)
    {
    }
}