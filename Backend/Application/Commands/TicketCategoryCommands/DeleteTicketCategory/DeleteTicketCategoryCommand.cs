using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.TicketCategoryCommands.DeleteTicketCategory;

public record DeleteTicketCatagoryCommand(TicketCategory TicketCatagory) : IRequest<Unit>;
