using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Todo.App.Commands;
using Todo.App.Models;
using Todo.Domain.AggregatesModel;
using ToDo.Domain.SeedWork;
using Xunit;

namespace Todo.App.UnitTests.Commands;

public sealed class CreateTodoItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenCommand_ShouldCreateItem()
    {
        const long id = 123L;
        var command = new CreateTodoItemCommand("1", true);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock.Setup(r => r.Add(It.IsAny<TodoItem>())).Returns<TodoItem>(item => item.SetId(id));
        repositoryMock.SetupGet(r => r.UnitOfWork).Returns(unitOfWorkMock.Object);
        var handler = new CreateTodoItemCommandHandler(repositoryMock.Object);

        var dto = await handler.Handle(command);

        dto.Should().BeEquivalentTo(new TodoItemDto(id, command.Name, command.IsComplete));
        repositoryMock.Verify(
            r => r.Add(It.Is<TodoItem>(i => i.Name == command.Name && i.IsComplete == command.IsComplete)),
            Times.Once);
        unitOfWorkMock.Verify(w => w.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
