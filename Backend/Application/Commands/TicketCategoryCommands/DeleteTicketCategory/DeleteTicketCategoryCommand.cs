using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.DeleteTicketCategory;

public record DeleteTicketCategoryCommand(
  int TicketCategoryId
) : IRequest<TicketCategoryDto>;
