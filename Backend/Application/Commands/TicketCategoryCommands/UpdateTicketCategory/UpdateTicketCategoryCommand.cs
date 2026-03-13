using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.UpdateTicketCategory;

public record UpdateTicketCatagoryCommand(TicketCategory TicketCatagory) : IRequest<Unit>;
