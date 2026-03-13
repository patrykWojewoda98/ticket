using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.UpdateTicketCategory;

public record UpdateTicketCatagoryCommand(TicketCategory TicketCatagory) : IRequest<Unit>;
