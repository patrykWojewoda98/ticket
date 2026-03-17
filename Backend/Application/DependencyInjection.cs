using System;
using System.Reflection;
using Application.Common.Behaviours;
using Application.Queries.BaseQueries.GetAllEntities;
using Application.Queries.BaseQueries.GetEntityById;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

static public class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    var assembly = Assembly.GetExecutingAssembly();

    services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
    services.AddTransient<IRequestHandler<GetAllEntitiesQuery<Domain.Entities.Account>, List<Domain.Entities.Account>>, GetAllEntitiesQueryHandler<Domain.Entities.Account>>();
    services.AddTransient<IRequestHandler<GetEntityByIdQuery<Domain.Entities.Account>, Domain.Entities.Account>, GetEntityByIdQueryHandler<Domain.Entities.Account>>();
    services.AddValidatorsFromAssembly(assembly);
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    return services;
  }
}
