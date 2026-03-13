using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.DeleteTicketCategory;

public record DeleteTicketCatagoryCommand(TicketCategory TicketCatagory) : IRequest<Unit>;
