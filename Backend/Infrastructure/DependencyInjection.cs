using System;
<<<<<<< HEAD
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.Extensions.DependencyInjection;
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
using Infrastructure.Context;
using Infrastructure.Services;
using Domain.Abstractions;
using Infrastructure.Repositories;

namespace Infrastructure;

static public class DependencyInjection
{
<<<<<<< HEAD
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DefaultConnection")
      ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new InvalidOperationException("Database connection string is not configured. Set ConnectionStrings:DefaultConnection in appsettings or DB_CONNECTION_STRING environment variable.");
    }

    services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
=======
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddDbContext<DatabaseContext>();
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
    services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    services.AddScoped<ICommentRepository, CommentRepository>();
    services.AddScoped<ICompanyRepository, CompanyRepository>();
    services.AddScoped<ITicketAttachmentRepository, TicketAttachmentRepository>();
    services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
    services.AddScoped<ITicketHistoryRepository, TicketHistoryRepository>();
    services.AddScoped<ITicketNotificationRepository, TicketNotificationRepository>();
    services.AddScoped<ITicketPriorityRepository, TicketPriorityRepository>();
    services.AddScoped<ITicketRepository, TicketRepository>();
    services.AddScoped<ITicketStatusRepository, TicketStatusRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IPasswordHasher, PasswordHasherService>();
    services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

    return services;
  }
}
