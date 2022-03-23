using Todo.Domain.AggregatesModel;

namespace Todo.App.Models;

public sealed class TodoItemDto
{
    public long Id { get; }

    public string Name { get; }

    public bool IsComplete { get; }

    public TodoItemDto(long id, string name, bool isComplete)
    {
        Id = id;
        Name = name;
        IsComplete = isComplete;
    }

    public static TodoItemDto Map(TodoItem item) => new(item.Id, item.Name, item.IsComplete);
}
