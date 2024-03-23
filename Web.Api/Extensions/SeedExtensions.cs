using Bogus;
using Web.Api.Common.Persistence;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Extensions;

public static class SeedExtensions
{
    public static void ApplySeeds(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var status = new[] { 1, 2, 3 };
        var categories = new[] { 1, 2, 3 };
        
        var tasks = new Faker<Todo>()
            .RuleFor(p => p.Description, v => v.Lorem.Sentence())
            .RuleFor(p => p.Id, v => v.Random.Guid())
            .RuleFor(p => p.DueDate, v => v.Date.Between(DateTime.Now, DateTime.Now).AddDays(6).ToString("MM/dd/yyyy"))
            .RuleFor(p => p.Category, v => TodoCategory.FromValue(v.PickRandom(categories)))
            .RuleFor(p => p.Status, v => TodoStatus.FromValue(v.PickRandom(status)));
        
        dbContext.Todos.AddRange( tasks.Generate(30));
        dbContext.SaveChanges();
    }
}