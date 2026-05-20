using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketPriorityQueries.GetTicketPriorityById;

public class GetTicketPriorityByIdQueryHandler : IRequestHandler<GetTicketPriorityByIdQuery, TicketPriorityDto>
{
  private readonly ITicketPriorityRepository _repository;

  public GetTicketPriorityByIdQueryHandler(ITicketPriorityRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketPriorityDto> Handle(GetTicketPriorityByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketPriority = await _repository.GetByIdAsync(request.Id);
    if (ticketPriority == null) return null;

    return new TicketPriorityDto
    {
      Id = ticketPriority.Id,
      Name = ticketPriority.Name
    };
  }
}
