using System.Reflection;
using Todo.Domain.AggregatesModel;
using ToDo.Domain.SeedWork;

namespace Todo.App.UnitTests;

internal static class TodoItemFactory
{
    private static readonly FieldInfo IdProperty = typeof(Entity)
        .GetField($"<{nameof(Entity.Id)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public static TodoItem SetId(this TodoItem item, long id)
    {
        IdProperty.SetValue(item, id);

        return item;
    }

    public static TodoItem Create(long id, string name, bool isComplete)
    {
        var item = new TodoItem(name);
        item.SetComplete(isComplete);
        item.SetId(id);

        return item;
    }
}
