using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.UpdateEntity;

public record UpdateEntityCommand<T>(T Entity) : IRequest<Unit> where T : Base;
