using Microsoft.EntityFrameworkCore;
using Todo.Domain.AggregatesModel;
using ToDo.Domain.SeedWork;

namespace Todo.Infrastructure.Repositories;

internal sealed class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoContext context;

    public IUnitOfWork UnitOfWork => context;

    public TodoItemRepository(TodoContext context) => this.context = context;

    public async Task<IEnumerable<TodoItem>> GetAsync(CancellationToken cancellationToken = default) =>
        await context.TodoItems.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<TodoItem?> GetAsync(long id, CancellationToken cancellationToken = default) =>
        await context.TodoItems.FindAsync(new object[] { id }, cancellationToken);

    public TodoItem Add(TodoItem item) => context.TodoItems.Add(item).Entity;

    public TodoItem Update(TodoItem item) => context.TodoItems.Update(item).Entity;

    public void Remove(TodoItem item) => context.TodoItems.Remove(item);
}
