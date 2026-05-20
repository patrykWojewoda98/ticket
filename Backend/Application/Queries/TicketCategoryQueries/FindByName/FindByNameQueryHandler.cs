using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.FindByName;

public class FindByNameQueryHandler : IRequestHandler<FindByNameQuery, TicketCategoryDto>
{
  private readonly ITicketCategoryRepository _repository;

  public FindByNameQueryHandler(ITicketCategoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<TicketCategoryDto> Handle(FindByNameQuery request, CancellationToken cancellationToken)
  {
    var ticketCategory = await _repository.FindByNameAsync(request.Name);
    if (ticketCategory == null) return null;

    return new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    };
  }
}
