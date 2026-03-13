using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.BaseCommands.CreateEntity;

public record CreateEntityCommand<T>(T Entity) : IRequest<Unit> where T : Base;
