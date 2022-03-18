using Microsoft.EntityFrameworkCore;
using Todo.Domain.AggregatesModel;
using ToDo.Domain.SeedWork;
using Todo.Infrastructure.EntityConfigurations;

namespace Todo.Infrastructure;

internal class TodoContext : DbContext, IUnitOfWork
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoItemEntityTypeConfiguration());
    }
}
