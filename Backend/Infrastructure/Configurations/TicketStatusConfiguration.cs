using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
{
  public void Configure(EntityTypeBuilder<TicketStatus> builder)
  {
    builder.ToTable("ticket_status");

    builder.HasKey(ticketStatus => ticketStatus.Id);
    builder.HasIndex(ticketStatus => ticketStatus.Id);
    builder.HasIndex(ticketStatus => ticketStatus.Name).IsUnique();

    builder.Property(ticketStatus => ticketStatus.Name).IsRequired();

    builder.HasMany(ticketStatus => ticketStatus.Tickets)
           .WithOne(ticket => ticket.Status)
           .HasForeignKey(ticket => ticket.StatusId);
  }
}
