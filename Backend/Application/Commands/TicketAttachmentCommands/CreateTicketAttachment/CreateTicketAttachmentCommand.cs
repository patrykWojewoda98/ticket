using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketAttachmentCommands.CreateTicketAttachment;

public record CreateTicketAttachmentCommand(
  int TicketId,
  string Filename,
  string Path,
  int? UploadedBy
) : IRequest<TicketAttachmentDto>;
