using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.GetTicketCategoryById;

public record GetTicketCategoryByIdQuery(int Id) : IRequest<TicketCategoryDto>;
