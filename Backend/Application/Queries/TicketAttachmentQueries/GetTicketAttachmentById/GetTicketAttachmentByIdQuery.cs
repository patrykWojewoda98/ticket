using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.GetTicketAttachmentById;

public record GetTicketAttachmentByIdQuery(int Id) : IRequest<TicketAttachmentDto>;
