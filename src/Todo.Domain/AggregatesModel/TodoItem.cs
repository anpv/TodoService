using Todo.Domain.Exceptions;
using ToDo.Domain.SeedWork;

namespace Todo.Domain.AggregatesModel;

public class TodoItem : Entity, IAggregateRoot
{
    public const int NameMaxLength = 250;

    public string Name { get; private set; }

    public bool IsComplete { get; private set; }

    public string Secret { get; }

    public TodoItem(string name)
    {
        Name = ValidateName(name);
        Secret = Guid.NewGuid().ToString("N");
    }

    public void SetName(string name) => Name = ValidateName(name);

    public void SetComplete(bool isComplete) => IsComplete = isComplete;

    private static string ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new TodoDomainException("Name must be not empty.");
        }

        if (name.Length > NameMaxLength)
        {
            throw new TodoDomainException($"Name can have a max of {NameMaxLength} characters.");
        }

        return name;
    }
}
