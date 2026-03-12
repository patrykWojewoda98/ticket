using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TicketConfiguration : BaseConfiguration<Ticket>
{
  public override void Configure(EntityTypeBuilder<Ticket> builder)
  {
    base.Configure(builder);

    builder.ToTable("ticket");
    builder.HasIndex(ticket => ticket.UserId);

    builder.Property(ticket => ticket.UserId).IsRequired();
    builder.Property(ticket => ticket.AssigneeId).IsRequired(false);
    builder.Property(ticket => ticket.CategoryId).IsRequired(false);
    builder.Property(ticket => ticket.StatusId).IsRequired();
    builder.Property(ticket => ticket.PriorityId).IsRequired();
    builder.Property(ticket => ticket.Title).IsRequired();
    builder.Property(ticket => ticket.Description)
           .IsRequired()
           .HasColumnType("text");

    builder.HasOne(ticket => ticket.User)
           .WithMany(user => user.Tickets)
           .HasForeignKey(ticket => ticket.UserId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ticket => ticket.Assignee)
           .WithMany(user => user.Assigned)
           .HasForeignKey(ticket => ticket.AssigneeId)
           .OnDelete(DeleteBehavior.SetNull);

    builder.HasOne(ticket => ticket.Status)
           .WithMany(ticketStatus => ticketStatus.Tickets)
           .HasForeignKey(ticket => ticket.StatusId);

    builder.HasOne(ticket => ticket.Priority)
           .WithMany(ticketPriority => ticketPriority.Tickets)
           .HasForeignKey(ticket => ticket.PriorityId);

    builder.HasOne(ticket => ticket.Category)
           .WithMany(ticketCategory => ticketCategory.Tickets)
           .HasForeignKey(ticket => ticket.CategoryId);

    builder.HasMany(ticket => ticket.Attachments)
           .WithOne(ticketAttachment => ticketAttachment.Ticket)
           .HasForeignKey(ticketAttachment => ticketAttachment.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(ticket => ticket.History)
           .WithOne(ticketHistory => ticketHistory.Ticket)
           .HasForeignKey(ticketHistory => ticketHistory.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(ticket => ticket.Notifications)
           .WithOne(ticketNotification => ticketNotification.Ticket)
           .HasForeignKey(ticketNotification => ticketNotification.TicketId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(ticket => ticket.Comments)
           .WithOne(comment => comment.Ticket)
           .HasForeignKey(comment => comment.TicketId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
