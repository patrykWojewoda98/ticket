using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.UpdateTicketHistory;

public record UpdateTicketHistoryCommand(TicketHistory TicketHistory) : IRequest<Unit>;
