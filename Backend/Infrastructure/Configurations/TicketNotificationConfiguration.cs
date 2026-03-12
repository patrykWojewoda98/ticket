using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketNotificationConfiguration : IEntityTypeConfiguration<TicketNotification>
{
  public void Configure(EntityTypeBuilder<TicketNotification> builder)
  {
    builder.ToTable("ticket_notification");

    builder.HasKey(ticketNotification => ticketNotification.Id);
    builder.HasIndex(ticketNotification => ticketNotification.TicketId);

    builder.Property(ticketNotification => ticketNotification.TicketId).IsRequired();
    builder.Property(ticketNotification => ticketNotification.UserId).IsRequired();
    builder.Property(ticketNotification => ticketNotification.Message).IsRequired();
    builder.Property(ticketNotification => ticketNotification.Read).HasDefaultValue(false);
    builder.Property(ticketNotification => ticketNotification.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasOne(ticketNotification => ticketNotification.Ticket)
           .WithMany(ticket => ticket.Notifications)
           .HasForeignKey(ticketNotification => ticketNotification.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ticketNotification => ticketNotification.User)
           .WithMany(user => user.TicketNotifications)
           .HasForeignKey(ticketNotification => ticketNotification.UserId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
