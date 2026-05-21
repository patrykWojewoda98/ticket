using Infrastructure;
using Application;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD
builder.Services.AddInfrastructure(builder.Configuration);
=======
builder.Services.AddInfrastructure();
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
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
