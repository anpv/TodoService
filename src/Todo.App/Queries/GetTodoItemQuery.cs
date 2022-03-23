using MediatR;
using Todo.App.Models;

namespace Todo.App.Queries;

public sealed class GetTodoItemQuery : IRequest<TodoItemDto>
{
    public long Id { get; }

    public GetTodoItemQuery(long id) => Id = id;
}
