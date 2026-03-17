using Infrastructure;
using Application;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddPresentation();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllOrigins", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UsePresentation();
app.Run();
