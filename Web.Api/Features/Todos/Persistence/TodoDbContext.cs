using Microsoft.EntityFrameworkCore;
using Web.Api.Features.Todos.Models;

// Disable as we want the partial class to be in the same namespace as the original class
// ReSharper disable once CheckNamespace
namespace Web.Api.Common.Persistence;

public partial class AppDbContext
{
    public DbSet<Todo> Todos { get; set; } = null!;
}