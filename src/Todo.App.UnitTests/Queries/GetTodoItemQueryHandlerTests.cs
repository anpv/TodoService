using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Todo.App.Exceptions;
using Todo.App.Models;
using Todo.App.Queries;
using Todo.Domain.AggregatesModel;
using Xunit;

namespace Todo.App.UnitTests.Queries;

public sealed class GetTodoItemQueryHandlerTests
{
    [Fact]
    public async Task Handle_WhenItemExist_ShouldReturnDto()
    {
        var item = TodoItemFactory.Create(123L, "1", true);
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(item.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        var handler = new GetTodoItemQueryHandler(repositoryMock.Object);
        var query = new GetTodoItemQuery(item.Id);

        var dto = await handler.Handle(query);

        dto.Should().BeEquivalentTo(new TodoItemDto(item.Id, item.Name, item.IsComplete));
    }

    [Fact]
    public async Task Handle_WhenItemNotExist_ShouldThrowException()
    {
        var query = new GetTodoItemQuery(123L);
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TodoItem?)null);
        var handler = new GetTodoItemQueryHandler(repositoryMock.Object);

        var action = () => handler.Handle(query);

        await action.Should().ThrowAsync<TodoItemNotFoundException>();
    }
}
