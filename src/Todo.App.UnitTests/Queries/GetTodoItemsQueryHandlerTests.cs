using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Todo.App.Models;
using Todo.App.Queries;
using Todo.Domain.AggregatesModel;
using Xunit;

namespace Todo.App.UnitTests.Queries;

public sealed class GetTodoItemsQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenItems_ShouldReturnDtos()
    {
        var item = TodoItemFactory.Create(123L, "1", true);
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { item });
        var handler = new GetTodoItemsQueryHandler(repositoryMock.Object);

        var dtos = await handler.Handle(GetTodoItemsQuery.Instance);

        dtos.Should().BeEquivalentTo(new[] { new TodoItemDto(item.Id, item.Name, item.IsComplete) });
    }

    [Fact]
    public async Task Handle_WhenNoItems_ShouldReturnEmpty()
    {
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<TodoItem>());
        var handler = new GetTodoItemsQueryHandler(repositoryMock.Object);

        var dtos = await handler.Handle(GetTodoItemsQuery.Instance);

        dtos.Should().BeEmpty();
    }
}
