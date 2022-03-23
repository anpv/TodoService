using Todo.Domain.Exceptions;

namespace Todo.App.Exceptions;

public sealed class TodoItemNotFoundException : TodoDomainException
{
    public TodoItemNotFoundException(long id) : base($"Todo item {id} is not found.")
    {
    }
}
