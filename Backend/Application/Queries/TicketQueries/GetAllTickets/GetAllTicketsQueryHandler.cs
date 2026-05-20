using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketQueries.GetAllTickets;

public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketDto>>
{
  private readonly ITicketRepository _repository;

  public GetAllTicketsQueryHandler(ITicketRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
  {
    var tickets = await _repository.GetAllAsync();
    return tickets.Select(ticket => new TicketDto
    {
      Id = ticket.Id,
      UserId = ticket.UserId,
      AssigneeId = ticket.AssigneeId,
      CategoryId = ticket.CategoryId,
      StatusId = ticket.StatusId,
      PriorityId = ticket.PriorityId,
      Title = ticket.Title,
      Description = ticket.Description
    }).ToList();
  }
}
