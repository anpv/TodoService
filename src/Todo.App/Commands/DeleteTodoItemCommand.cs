using MediatR;

namespace Todo.App.Commands;

public sealed class DeleteTodoItemCommand : IRequest<Unit>
{
    public long Id { get; }

    public DeleteTodoItemCommand(long id) => Id = id;
}
