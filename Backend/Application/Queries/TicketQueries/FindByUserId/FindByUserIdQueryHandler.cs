using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketQueries.FindByUserId;

public class FindByUserIdQueryHandler : IRequestHandler<FindByUserIdQuery, List<TicketDto>>
{
  private readonly ITicketRepository _repository;

  public FindByUserIdQueryHandler(ITicketRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketDto>> Handle(FindByUserIdQuery request, CancellationToken cancellationToken)
  {
    var tickets = await _repository.FindByUserIdAsync(request.TicketId);
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
