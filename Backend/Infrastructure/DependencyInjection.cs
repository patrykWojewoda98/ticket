using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Context;
using Infrastructure.Services;
using Domain.Abstractions;
using Infrastructure.Repositories;

namespace Infrastructure;

static public class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<DatabaseContext>();
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    services.AddScoped<ICommentRepository, CommentRepository>();
    services.AddScoped<ICompanyRepository, CompanyRepository>();
    services.AddScoped<ISessionRepository, SessionRepository>();
    services.AddScoped<ITicketAttachmentRepository, TicketAttachmentRepository>();
    services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
    services.AddScoped<ITicketHistoryRepository, TicketHistoryRepository>();
    services.AddScoped<ITicketNotificationRepository, TicketNotificationRepository>();
    services.AddScoped<ITicketPriorityRepository, TicketPriorityRepository>();
    services.AddScoped<ITicketRepository, TicketRepository>();
    services.AddScoped<ITicketStatusRepository, TicketStatusRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

    return services;
  }
}
