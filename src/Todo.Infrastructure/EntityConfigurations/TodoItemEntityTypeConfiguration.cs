using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.AggregatesModel;

namespace Todo.Infrastructure.EntityConfigurations;

internal class TodoItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItem");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).HasMaxLength(TodoItem.NameMaxLength).IsRequired();
        builder.Property(i => i.IsComplete);
        builder.Property(i => i.Secret).IsRequired();
    }
}
