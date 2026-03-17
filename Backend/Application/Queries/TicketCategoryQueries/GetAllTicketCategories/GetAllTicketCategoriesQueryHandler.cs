using System;
using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.GetAllTicketCategories;

public class GetAllTicketCategoriesQueryHandler : IRequestHandler<GetAllTicketCategoriesQuery, List<TicketCategoryDto>>
{
  private readonly ITicketCategoryRepository _repository;

  public GetAllTicketCategoriesQueryHandler(ITicketCategoryRepository repository)
  {
    _repository = repository;
  }

  public async Task<List<TicketCategoryDto>> Handle(GetAllTicketCategoriesQuery request, CancellationToken cancellationToken)
  {
    var ticketCategories = await _repository.GetAllAsync();
    return ticketCategories.Select(ticketCategory => new TicketCategoryDto
    {
      Id = ticketCategory.Id,
      Name = ticketCategory.Name
    }).ToList();
  }
}
