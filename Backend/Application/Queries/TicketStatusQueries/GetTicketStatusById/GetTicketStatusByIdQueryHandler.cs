using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketStatusQueries.GetTicketStatusById;

public class GetTicketStatusByIdQueryHandler : IRequestHandler<GetTicketStatusByIdQuery, TicketStatusDto>
{
  private readonly ITicketStatusRepository _repository;

  public GetTicketStatusByIdQueryHandler(ITicketStatusRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketStatusDto> Handle(GetTicketStatusByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketStatus = await _repository.GetByIdAsync(request.Id);
    if (ticketStatus == null) return null;

    return new TicketStatusDto
    {
      Id = ticketStatus.Id,
      Name = ticketStatus.Name
    };
  }
}
