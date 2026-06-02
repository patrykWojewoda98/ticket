using Infrastructure;
using Application;
using Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddPresentation();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllOrigins", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
  try
  {
    logger.LogInformation("Applying database migrations...");
    var dbContext = scope.ServiceProvider.GetRequiredService<Infrastructure.Context.DatabaseContext>();
    dbContext.Database.Migrate();
    logger.LogInformation("Database migrations applied successfully.");
  }
  catch (Exception ex)
  {
    logger.LogError(ex, "An error occurred while applying the database migrations.");
  }
}

app.UseCors("AllowAllOrigins");
app.UsePresentation();
app.Run();
