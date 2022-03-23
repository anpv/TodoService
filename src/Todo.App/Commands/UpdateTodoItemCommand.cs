using MediatR;

namespace Todo.App.Commands;

public sealed class UpdateTodoItemCommand : IRequest<Unit>
{
    public long Id { get; }

    public string Name { get; }

    public bool IsComplete { get; }

    public UpdateTodoItemCommand(long id, string name, bool isComplete)
    {
        Id = id;
        Name = name;
        IsComplete = isComplete;
    }
}
