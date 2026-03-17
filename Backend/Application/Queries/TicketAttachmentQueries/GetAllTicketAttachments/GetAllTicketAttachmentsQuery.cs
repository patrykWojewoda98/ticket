using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.GetAllTicketAttachments;

public record GetAllTicketAttachmentsQuery() : IRequest<List<TicketAttachmentDto>>;
