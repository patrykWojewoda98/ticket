using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketCategoryConfiguration : BaseConfiguration<TicketCategory>
{
  public override void Configure(EntityTypeBuilder<TicketCategory> builder)
  {
    base.Configure(builder);

    builder.ToTable("ticket_category");
    builder.HasIndex(ticketCategory => ticketCategory.Id);
    builder.HasIndex(ticketCategory => ticketCategory.Name).IsUnique();

    builder.Property(ticketCategory => ticketCategory.Name).IsRequired();

    builder.HasMany(ticketCategory => ticketCategory.Tickets)
           .WithOne(ticket => ticket.Category)
           .HasForeignKey(ticket => ticket.CategoryId);
  }
}
