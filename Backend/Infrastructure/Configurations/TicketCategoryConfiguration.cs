using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
{
  public void Configure(EntityTypeBuilder<TicketCategory> builder)
  {
    builder.ToTable("ticket_category");

    builder.HasKey(ticketCategory => ticketCategory.Id);
    builder.HasIndex(ticketCategory => ticketCategory.Id);
    builder.HasIndex(ticketCategory => ticketCategory.Name).IsUnique();

    builder.Property(ticketCategory => ticketCategory.Name).IsRequired();

    builder.HasMany(ticketCategory => ticketCategory.Tickets)
           .WithOne(ticket => ticket.Category)
           .HasForeignKey(ticket => ticket.CategoryId);
  }
}
