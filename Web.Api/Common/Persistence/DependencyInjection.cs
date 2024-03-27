using Microsoft.EntityFrameworkCore;

namespace Web.Api.Common.Persistence;

public static class DependencyInjection
{
    public static void AddEfCore(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseInMemoryDatabase("InMemoryDbForTesting");
        });
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
    }
}