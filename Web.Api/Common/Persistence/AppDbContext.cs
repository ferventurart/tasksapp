using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Common.Persistence;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}