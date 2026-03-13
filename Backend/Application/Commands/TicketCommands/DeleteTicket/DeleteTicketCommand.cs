using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCommands.DeleteTicket;

public record DeleteTicketCommand(Ticket Ticket) : IRequest<Unit>;
