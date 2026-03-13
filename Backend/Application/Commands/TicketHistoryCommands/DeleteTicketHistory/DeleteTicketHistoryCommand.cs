using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.DeleteTicketHistory;

public record DeleteTicketHistoryCommand(TicketHistory TicketHistory) : IRequest<Unit>;
