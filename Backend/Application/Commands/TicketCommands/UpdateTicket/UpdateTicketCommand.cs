using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCommands.UpdateTicket;

public record UpdateTicketCommand(Ticket Ticket) : IRequest<Unit>;
