using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketHistoryConfiguration : BaseConfiguration<TicketHistory>
{
  public override void Configure(EntityTypeBuilder<TicketHistory> builder)
  {
    base.Configure(builder);

    builder.ToTable("ticket_history");
    builder.HasIndex(ticketHistory => ticketHistory.TicketId);

    builder.Property(ticketHistory => ticketHistory.TicketId).IsRequired();
    builder.Property(ticketHistory => ticketHistory.Action).IsRequired();
    builder.Property(ticketHistory => ticketHistory.OldValue).IsRequired(false);
    builder.Property(ticketHistory => ticketHistory.NewValue).IsRequired(false);
    builder.Property(ticketHistory => ticketHistory.UserId).IsRequired(false);

    builder.HasOne(ticketHistory => ticketHistory.Ticket)
           .WithMany(ticket => ticket.History)
           .HasForeignKey(ticketHistory => ticketHistory.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ticketHistory => ticketHistory.User)
           .WithMany(user => user.TicketHistories)
           .HasForeignKey(ticketHistory => ticketHistory.UserId)
           .OnDelete(DeleteBehavior.SetNull);
  }
}
