namespace Web.Api.Features.Todos.Errors;

public static class TodoErrorCodes
{
    public static class CreateTodo
    {
        public const string MissingDescription = nameof(MissingDescription);
        public const string DescriptionInvalidLength = nameof(DescriptionInvalidLength);
        public const string InvalidDueDate = nameof(InvalidDueDate);
        public const string MissingCategory = nameof(MissingCategory);
    }
    
    public static class UpdateTodo
    {
        public const string MissingId = nameof(MissingId);
        public const string MissingStatus = nameof(MissingStatus);
    }
}