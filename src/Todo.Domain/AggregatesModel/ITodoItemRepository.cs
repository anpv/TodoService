using ToDo.Domain.SeedWork;

namespace Todo.Domain.AggregatesModel;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    Task<IEnumerable<TodoItem>> GetAsync(CancellationToken cancellationToken = default);

    Task<TodoItem?> GetAsync(long id, CancellationToken cancellationToken = default);

    TodoItem Add(TodoItem item);

    TodoItem Update(TodoItem item);

    void Remove(TodoItem item);
}
