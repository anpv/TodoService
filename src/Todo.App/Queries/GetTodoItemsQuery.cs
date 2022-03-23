using MediatR;
using Todo.App.Models;

namespace Todo.App.Queries;

public sealed class GetTodoItemsQuery : IRequest<IEnumerable<TodoItemDto>>
{
    public static readonly GetTodoItemsQuery Instance = new();
}
