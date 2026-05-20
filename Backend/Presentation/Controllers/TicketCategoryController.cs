using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketCategoryCommands.CreateTicketCategory;
using Application.Commands.TicketCategoryCommands.DeleteTicketCategory;
using Application.Commands.TicketCategoryCommands.UpdateTicketCategory;
using Application.Queries.TicketCategoryQueries.GetAllTicketCategories;
using Application.Queries.TicketCategoryQueries.GetTicketCategoryById;
using Application.Queries.TicketCategoryQueries.FindByName;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketCategoryController : BaseController
{
  public TicketCategoryController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create ticket category")]
  [ProducesResponseType(typeof(TicketCategoryDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketCategoryCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketCategoryById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket category")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCategoryCommand command)
  {
    var result = await _mediator.Send(command with { TicketCategoryId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket category")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketCategoryCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket categories")]
  public async Task<ActionResult<List<TicketCategoryDto>>> GetAllTicketCategories()
  {
    var result = await _mediator.Send(new GetAllTicketCategoriesQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket category by unique record ID")]
  public async Task<ActionResult<TicketCategoryDto>> GetTicketCategoryById(int id)
  {
    var result = await _mediator.Send(new GetTicketCategoryByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("name/{name}")]
  [SwaggerOperation(Summary = "Find ticket category by its name")]
  public async Task<ActionResult<TicketCategoryDto>> FindByName(string name)
  {
    var result = await _mediator.Send(new FindByNameQuery(name));
    return (result == null) ? NotFound() : Ok(result);
  }
}
