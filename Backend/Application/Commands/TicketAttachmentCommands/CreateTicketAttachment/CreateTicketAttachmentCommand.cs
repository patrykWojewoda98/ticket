using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.CreateTicketAttachment;

public record CreateTicketAttachmentCommand(TicketAttachment TicketAttachment) : IRequest<Unit>;
