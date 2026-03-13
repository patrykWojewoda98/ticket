using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.DeleteTicketAttachment;

public record DeleteTicketAttachmentCommand(TicketAttachment TicketAttachment) : IRequest<Unit>;
