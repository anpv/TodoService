using MediatR;
using Todo.App.Exceptions;
using Todo.Domain.AggregatesModel;

namespace Todo.App.Commands;

internal sealed class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemRepository repository;

    public DeleteTodoItemCommandHandler(ITodoItemRepository repository) => this.repository = repository;

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken token = default)
    {
        var item = await repository.GetAsync(request.Id, token);
        if (item == null)
        {
            throw new TodoItemNotFoundException(request.Id);
        }

        repository.Remove(item);
        await repository.UnitOfWork.SaveEntitiesAsync(token);

        return Unit.Value;
    }
}
