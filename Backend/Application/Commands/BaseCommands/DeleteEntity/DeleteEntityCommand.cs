using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.DeleteEntity;

public record DeleteEntityCommand<T>(T Entity) : IRequest<Unit> where T : Base;
