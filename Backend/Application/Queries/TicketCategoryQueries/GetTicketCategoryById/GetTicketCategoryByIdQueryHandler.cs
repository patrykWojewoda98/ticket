using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.GetTicketCategoryById;

public class GetTicketCategoryByIdQueryHandler : IRequestHandler<GetTicketCategoryByIdQuery, TicketCategoryDto>
{
  private readonly ITicketCategoryRepository _repository;

  public GetTicketCategoryByIdQueryHandler(ITicketCategoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketCategoryDto> Handle(GetTicketCategoryByIdQuery request, CancellationToken cancellationToken)
  {
    var ticketCategory = await _repository.GetByIdAsync(request.Id);
    if (ticketCategory == null) return null;

    return new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    };
  }
}
