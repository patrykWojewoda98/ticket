using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.CreateTicketCategory;

public record CreateTicketCatagoryCommand(TicketCategory TicketCatagory) : IRequest<Unit>;
