using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation;

static public class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(swagger => { swagger.EnableAnnotations(); });
    services.AddControllers();

    return services;
  }

  public static IApplicationBuilder UsePresentation(this WebApplication app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapControllers();

    return app;
  }
}
