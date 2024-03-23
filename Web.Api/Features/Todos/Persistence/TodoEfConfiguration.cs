using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Features.Todos.Models;

namespace Web.Api.Features.Todos.Persistence;

public class TodoEfConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1024);
        builder.Property(x => x.DueDate)
            .IsRequired(false)
            .HasMaxLength(10);

        builder.Property(x => x.Category).HasConversion(
            category => category.Value,
            value => TodoCategory.FromValue(value)!);
        
        builder.Property(x => x.Status).HasConversion(
            status => status.Value,
            value => TodoStatus.FromValue(value)!);
    }
}