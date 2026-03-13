using System;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BaseQueries.GetEntityById;

public record GetEntityByIdQuery<T>(int Id) : IRequest<T?> where T : Base;
