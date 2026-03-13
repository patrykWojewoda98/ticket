using System;
using Domain.Entities;
using MediatR;

namespace Application.Queries.BaseQueries.GetAllEntities;

public record GetAllEntitiesQuery<T>() : IRequest<List<T>> where T : Base;

