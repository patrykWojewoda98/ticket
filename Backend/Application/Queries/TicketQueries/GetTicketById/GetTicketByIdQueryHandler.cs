using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketQueries.GetTicketById;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
  private readonly ITicketRepository _repository;

  public GetTicketByIdQueryHandler(ITicketRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
  {
    var ticket = await _repository.GetByIdAsync(request.Id);
    if (ticket == null) return null;

    return new TicketDto
    {
      Id = ticket.Id,
      UserId = ticket.UserId,
      AssigneeId = ticket.AssigneeId,
      CategoryId = ticket.CategoryId,
      StatusId = ticket.StatusId,
      PriorityId = ticket.PriorityId,
      Title = ticket.Title,
      Description = ticket.Description
    };
  }
}
