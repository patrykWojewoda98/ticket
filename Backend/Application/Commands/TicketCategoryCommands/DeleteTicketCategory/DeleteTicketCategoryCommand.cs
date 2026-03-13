using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.DeleteTicketCategory;

public record DeleteTicketCatagoryCommand(
  int TicketCategoryId
) : IRequest<TicketCategoryDto>;
