using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketHistoryCommands.CreateTicketHistory;

public record CreateTicketHistoryCommand(TicketHistory TicketHistory) : IRequest<Unit>;
