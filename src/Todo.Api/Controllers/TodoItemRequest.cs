using System.ComponentModel.DataAnnotations;
using Todo.Domain.AggregatesModel;

namespace ToDo.Api.Controllers;

public sealed class TodoItemRequest
{
    [StringLength(TodoItem.NameMaxLength, MinimumLength = 1)]
    public string Name { get; init; } = null!;

    public bool IsComplete { get; init; }
}
