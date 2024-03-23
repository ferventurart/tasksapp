using Web.Api.Common.Models;

namespace Web.Api.Features.Todos.Errors;

public static class TodoErrors
{
    public static Error NotFound(Guid todoId) => Error.NotFound(
        "Todo.NotFound", 
        $"The todo with the Id '{todoId}' was not found");
    
    public static Error NotValidStatus(string status) => Error.Conflict(
        "Todo.NotValidStatus", 
        $"The status {status} is not a valid value.");
    
    public static Error NotValidCategory(string category) => Error.Conflict(
        "Todo.NotValidCategory", 
        $"The category {category} is not a valid value.");
}