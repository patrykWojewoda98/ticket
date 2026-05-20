using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.GetAllTicketCategories;

public record GetAllTicketCategoriesQuery() : IRequest<List<TicketCategoryDto>>;
