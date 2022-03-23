using MediatR;
using Todo.App.Exceptions;
using Todo.App.Models;
using Todo.Domain.AggregatesModel;

namespace Todo.App.Queries;

internal sealed class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemDto>
{
    private readonly ITodoItemRepository repository;

    public GetTodoItemQueryHandler(ITodoItemRepository repository) => this.repository = repository;

    public async Task<TodoItemDto> Handle(GetTodoItemQuery request, CancellationToken token = default)
    {
        var item = await repository.GetAsync(request.Id, token);
        if (item == null)
        {
            throw new TodoItemNotFoundException(request.Id);
        }

        return TodoItemDto.Map(item);
    }
}
