using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketCategoryQueries.FindByName;

public record FindByNameQuery(string Name) : IRequest<TicketCategoryDto>;
