using MediatR;
using Todo.App.Exceptions;
using Todo.Domain.AggregatesModel;

namespace Todo.App.Commands;

internal sealed class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly ITodoItemRepository repository;

    public UpdateTodoItemCommandHandler(ITodoItemRepository repository) => this.repository = repository;

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken token = default)
    {
        var item = await repository.GetAsync(request.Id, token);
        if (item == null)
        {
            throw new TodoItemNotFoundException(request.Id);
        }

        item.SetName(request.Name);
        item.SetComplete(request.IsComplete);
        repository.Update(item);
        await repository.UnitOfWork.SaveEntitiesAsync(token);

        return Unit.Value;
    }
}
