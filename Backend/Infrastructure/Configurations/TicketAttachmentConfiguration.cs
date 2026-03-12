using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
{
  public void Configure(EntityTypeBuilder<TicketAttachment> builder)
  {
    builder.ToTable("ticket_attachment");

    builder.HasKey(ticketAttachment => ticketAttachment.Id);
    builder.HasIndex(ticketAttachment => ticketAttachment.TicketId);

    builder.Property(ticketAttachment => ticketAttachment.TicketId).IsRequired();
    builder.Property(ticketAttachment => ticketAttachment.Filename).IsRequired();
    builder.Property(ticketAttachment => ticketAttachment.Path).IsRequired();
    builder.Property(ticketAttachment => ticketAttachment.UploadedBy).IsRequired(false);
    builder.Property(ticketAttachment => ticketAttachment.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasOne(ticketAttachment => ticketAttachment.Ticket)
           .WithMany(ticket => ticket.Attachments)
           .HasForeignKey(ticketAttachment => ticketAttachment.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ticketAttachment => ticketAttachment.User)
           .WithMany(user => user.TicketAttachments)
           .HasForeignKey(ticketAttachment => ticketAttachment.UploadedBy)
           .OnDelete(DeleteBehavior.SetNull);
  }
}
