using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.UpdateTicketAttachment;

public record UpdateTicketAttachmentCommand(TicketAttachment TicketAttachment) : IRequest<Unit>;
