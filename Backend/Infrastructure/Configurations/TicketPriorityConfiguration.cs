using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketPriorityConfiguration : BaseConfiguration<TicketPriority>
{
  public override void Configure(EntityTypeBuilder<TicketPriority> builder)
  {
    base.Configure(builder);

    builder.ToTable("ticket_priority");
    builder.HasIndex(ticketPriority => ticketPriority.Id);
    builder.HasIndex(ticketPriority => ticketPriority.Name).IsUnique();

    builder.Property(ticketPriority => ticketPriority.Name).IsRequired();

    builder.HasMany(ticketPriority => ticketPriority.Tickets)
           .WithOne(ticket => ticket.Priority)
           .HasForeignKey(ticket => ticket.PriorityId);
  }
}
