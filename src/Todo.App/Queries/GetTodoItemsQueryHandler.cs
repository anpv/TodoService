using MediatR;
using Todo.App.Models;
using Todo.Domain.AggregatesModel;

namespace Todo.App.Queries;

internal sealed class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, IEnumerable<TodoItemDto>>
{
    private readonly ITodoItemRepository repository;

    public GetTodoItemsQueryHandler(ITodoItemRepository repository) => this.repository = repository;

    public async Task<IEnumerable<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken token = default)
    {
        var items = await repository.GetAsync(token);

        return items.Select(TodoItemDto.Map);
    }
}
