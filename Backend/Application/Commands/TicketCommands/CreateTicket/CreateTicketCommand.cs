using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCommands.CreateTicket;

public record CreateTicketCommand(Ticket Ticket) : IRequest<Unit>;
