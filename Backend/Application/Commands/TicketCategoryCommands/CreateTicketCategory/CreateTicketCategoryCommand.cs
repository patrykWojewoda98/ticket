using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.CreateTicketCategory;

public record CreateTicketCategoryCommand(
  string Name
) : IRequest<TicketCategoryDto>;
