using System;
using Application.Dtos;
using MediatR;

namespace Application.Queries.TicketAttachmentQueries.FindByTicketId;

public record FindByTicketIdQuery(int TicketId) : IRequest<List<TicketAttachmentDto>>;
