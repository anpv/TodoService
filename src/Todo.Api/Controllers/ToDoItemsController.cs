using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.App.Commands;
using Todo.App.Models;
using Todo.App.Queries;

namespace ToDo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ToDoItemsController : ControllerBase
{
    private readonly IMediator mediator;

    public ToDoItemsController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<TodoItemDto>> GetAll(CancellationToken token) =>
        await mediator.Send(GetTodoItemsQuery.Instance, token);

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<TodoItemDto> Get(long id, CancellationToken token)
    {
        var query = new GetTodoItemQuery(id);

        return await mediator.Send(query, token);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItemDto>> Create(TodoItemRequest request, CancellationToken token)
    {
        var command = new CreateTodoItemCommand(request.Name, request.IsComplete);
        var item = await mediator.Send(command, token);

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task Update(long id, TodoItemRequest request, CancellationToken token)
    {
        var command = new UpdateTodoItemCommand(id, request.Name, request.IsComplete);
        await mediator.Send(command, token);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task Delete(long id, CancellationToken token)
    {
        var command = new DeleteTodoItemCommand(id);
        await mediator.Send(command, token);
    }
}
