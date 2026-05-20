using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketQueries.FindByAssigneeId;

public class FindByAssigneeIdQueryHandler : IRequestHandler<FindByAssigneeIdQuery, List<TicketDto>>
{
  private readonly ITicketRepository _repository;

  public FindByAssigneeIdQueryHandler(ITicketRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketDto>> Handle(FindByAssigneeIdQuery request, CancellationToken cancellationToken)
  {
    var tickets = await _repository.FindByAssigneeIdAsync(request.AssigneeId);
    return tickets.Select(ticket => new TicketDto
    {
      Id = ticket.Id,
      UserId = ticket.UserId,
      AssigneeId = ticket.AssigneeId,
      CategoryId = ticket.CategoryId,
      StatusId = ticket.StatusId,
      PriorityId = ticket.PriorityId,
      Title = ticket.Title,
      Description = ticket.Description,
    }).ToList();
  }
}
