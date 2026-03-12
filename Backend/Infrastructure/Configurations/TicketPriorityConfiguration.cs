using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketPriorityConfiguration : IEntityTypeConfiguration<TicketPriority>
{
  public void Configure(EntityTypeBuilder<TicketPriority> builder)
  {
    builder.ToTable("ticket_priority");

    builder.HasKey(ticketPriority => ticketPriority.Id);
    builder.HasIndex(ticketPriority => ticketPriority.Id);
    builder.HasIndex(ticketPriority => ticketPriority.Name).IsUnique();

    builder.Property(ticketPriority => ticketPriority.Name).IsRequired();

    builder.HasMany(ticketPriority => ticketPriority.Tickets)
           .WithOne(ticket => ticket.Priority)
           .HasForeignKey(ticket => ticket.PriorityId);
  }
}
