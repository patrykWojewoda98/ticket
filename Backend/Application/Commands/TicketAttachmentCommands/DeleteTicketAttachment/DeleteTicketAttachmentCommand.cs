using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.DeleteTicketAttachment;

public record DeleteTicketAttachmentCommand(
  int TicketAttachmentId
) : IRequest<TicketAttachmentDto>;
