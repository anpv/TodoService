using MediatR;
using Todo.App.Models;
using Todo.Domain.AggregatesModel;

namespace Todo.App.Commands;

internal sealed class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
{
    private readonly ITodoItemRepository repository;

    public CreateTodoItemCommandHandler(ITodoItemRepository repository) => this.repository = repository;

    public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken token = default)
    {
        var item = new TodoItem(request.Name);
        item.SetComplete(request.IsComplete);
        item = repository.Add(item);
        await repository.UnitOfWork.SaveEntitiesAsync(token);

        return TodoItemDto.Map(item);
    }
}
