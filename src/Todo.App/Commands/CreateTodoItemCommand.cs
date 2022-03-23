using MediatR;
using Todo.App.Models;

namespace Todo.App.Commands;

public sealed class CreateTodoItemCommand : IRequest<TodoItemDto>
{
    public string Name { get; }

    public bool IsComplete { get; }

    public CreateTodoItemCommand(string name, bool isComplete)
    {
        Name = name;
        IsComplete = isComplete;
    }
}
