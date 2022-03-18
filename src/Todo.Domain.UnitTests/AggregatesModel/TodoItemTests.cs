using System.Collections.Generic;
using FluentAssertions;
using Todo.Domain.AggregatesModel;
using Todo.Domain.Exceptions;
using Xunit;

namespace Todo.Domain.UnitTests.AggregatesModel;

public sealed class TodoItemTests
{
    private static readonly string TooLongName = new('*', TodoItem.NameMaxLength + 1);

    [Fact]
    public void Ctor_WhenValidName_ShouldCreate()
    {
        var item = new TodoItem("1");

        item.Id.Should().Be(0L);
        item.Name.Should().Be("1");
        item.IsComplete.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(GetInvalidNames))]
    public void Ctor_WhenInvalidName_ShouldThrowException(string name)
    {
        var action = () => new TodoItem(name);

        action.Should().Throw<TodoDomainException>();
    }

    [Fact]
    public void SetName_WhenValidName_ShouldChange()
    {
        var item = new TodoItem("1");

        item.SetName("2");

        item.Name.Should().Be("2");
    }

    [Theory]
    [MemberData(nameof(GetInvalidNames))]
    public void SetName_WhenInvalidName_ShouldThrowException(string name)
    {
        var item = new TodoItem("1");

        var action = () => item.SetName(name);

        action.Should().Throw<TodoDomainException>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SetCompleted_WhenValue_ShouldSet(bool isComplete)
    {
        var item = new TodoItem("1");

        item.SetComplete(isComplete);

        item.IsComplete.Should().Be(isComplete);
    }

    private static IEnumerable<object[]> GetInvalidNames()
    {
        yield return new object[] { "" };
        yield return new object[] { null! };
        yield return new object[] { TooLongName };
    }
}
