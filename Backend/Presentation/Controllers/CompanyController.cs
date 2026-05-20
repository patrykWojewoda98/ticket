using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.CompanyCommands.CreateCompany;
using Application.Commands.CompanyCommands.DeleteCompany;
using Application.Commands.CompanyCommands.UpdateCompany;
using Application.Queries.CompanyQueries.GetAllCompanies;
using Application.Queries.CompanyQueries.GetCompanyById;
using Application.Queries.CompanyQueries.FindByUserId;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class CompanyController : BaseController
{
  public CompanyController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create company")]
  [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetCompanyById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update company")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
  {
    var result = await _mediator.Send(command with { CompanyId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete company")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteCompanyCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all companies")]
  public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
  {
    var result = await _mediator.Send(new GetAllCompaniesQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get company by unique record ID")]
  public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
  {
    var result = await _mediator.Send(new GetCompanyByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("user/{userId}")]
  [SwaggerOperation(Summary = "Get all companies associated with a specific user")]
  public async Task<ActionResult<List<CompanyDto>>> FindByUserId(int userId)
  {
    var result = await _mediator.Send(new FindByUserIdQuery(userId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
