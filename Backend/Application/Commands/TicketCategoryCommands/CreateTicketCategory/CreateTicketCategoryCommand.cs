using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.CreateTicketCategory;

public record CreateTicketCatagoryCommand(
  string Name
) : IRequest<TicketCategoryDto>;
