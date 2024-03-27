using Web.Api.Common.Features;
using Web.Api.Features.Todos.Persistence;

namespace Web.Api.Features.Todos;

public sealed class TodoFeature : IFeature
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<TodoRepository>();
        services.AddScoped<ITodoRepository, CachedTodoRepository>();
    }
}