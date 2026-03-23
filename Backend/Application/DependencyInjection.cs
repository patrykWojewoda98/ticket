using System;
using System.Reflection;
using Application.Common.Behaviours;
using Application.Dtos;
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
    services.AddTransient<IRequestHandler<Queries.CommentQueries.GetAllComments.GetAllCommentsQuery, List<CommentDto>>, Queries.CommentQueries.GetAllComments.GetAllCommentsQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.CompanyQueries.GetAllCompanies.GetAllCompaniesQuery, List<CompanyDto>>, Queries.CompanyQueries.GetAllCompanies.GetAllCompaniesQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketAttachmentQueries.GetAllTicketAttachments.GetAllTicketAttachmentsQuery, List<TicketAttachmentDto>>, Queries.TicketAttachmentQueries.GetAllTicketAttachments.GetAllTicketAttachmentsQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketCategoryQueries.GetAllTicketCategories.GetAllTicketCategoriesQuery, List<TicketCategoryDto>>, Queries.TicketCategoryQueries.GetAllTicketCategories.GetAllTicketCategoriesQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketHistoryQueries.GetAllTicketHistories.GetAllTicketHistoriesQuery, List<TicketHistoryDto>>, Queries.TicketHistoryQueries.GetAllTicketHistories.GetAllTicketHistoriesQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketNotificationQueries.GetAllTicketNotifications.GetAllTicketNotificationsQuery, List<TicketNotificationDto>>, Queries.TicketNotificationQueries.GetAllTicketNotifications.GetAllTicketNotificationsQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketPriorityQueries.GetAllTicketPriorities.GetAllTicketPrioritiesQuery, List<TicketPriorityDto>>, Queries.TicketPriorityQueries.GetAllTicketPriorities.GetAllTicketPrioritiesQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketQueries.GetAllTickets.GetAllTicketsQuery, List<TicketDto>>, Queries.TicketQueries.GetAllTickets.GetAllTicketsQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketStatusQueries.GetAllTicketStatuses.GetAllTicketStatusesQuery, List<TicketStatusDto>>, Queries.TicketStatusQueries.GetAllTicketStatuses.GetAllTicketStatusesQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.TicketStatusQueries.GetTicketStatusById.GetTicketStatusByIdQuery, TicketStatusDto>, Queries.TicketStatusQueries.GetTicketStatusById.GetTicketStatusByIdQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.UserQueries.GetAllUsers.GetAllUsersQuery, List<UserDto>>, Queries.UserQueries.GetAllUsers.GetAllUsersQueryHandler>();
    services.AddTransient<IRequestHandler<Queries.UserQueries.GetUserById.GetUserByIdQuery, UserDto>, Queries.UserQueries.GetUserById.GetUserByIdQueryHandler>();
    services.AddValidatorsFromAssembly(assembly);
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    return services;
  }
}
