using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Todo.App.Commands;
using Todo.App.Exceptions;
using Todo.Domain.AggregatesModel;
using ToDo.Domain.SeedWork;
using Xunit;

namespace Todo.App.UnitTests.Commands;

public sealed class DeleteTodoItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenItemFound_ShouldDelete()
    {
        var item = TodoItemFactory.Create(123L, "1", false);
        var command = new DeleteTodoItemCommand(123L);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);
        repositoryMock.SetupGet(r => r.UnitOfWork).Returns(unitOfWorkMock.Object);
        var handler = new DeleteTodoItemCommandHandler(repositoryMock.Object);

        await handler.Handle(command);

        repositoryMock.Verify(r => r.GetAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(r => r.Remove(item), Times.Once);
        unitOfWorkMock.Verify(w => w.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenItemNotFound_ShouldThrowException()
    {
        var command = new DeleteTodoItemCommand(123L);
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(r => r.GetAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TodoItem?)null);
        var handler = new DeleteTodoItemCommandHandler(repositoryMock.Object);

        var action = () => handler.Handle(command);

        await action.Should().ThrowAsync<TodoItemNotFoundException>();
    }
}
