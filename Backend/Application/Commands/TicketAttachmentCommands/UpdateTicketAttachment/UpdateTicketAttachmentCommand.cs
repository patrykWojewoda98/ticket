using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.UpdateTicketAttachment;

public record UpdateTicketAttachmentCommand(
  int TicketAttachmentId,
  string? Filename,
  string? Path,
  string? UploadedBy
) : IRequest<TicketAttachmentDto>;
