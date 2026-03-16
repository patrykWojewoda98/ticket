using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.UpdateTicketCategory;

public record UpdateTicketCategoryCommand(
  int TicketCategoryId,
  string? Name
) : IRequest<TicketCategoryDto>;
