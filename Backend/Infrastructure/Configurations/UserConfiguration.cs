using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
  public override void Configure(EntityTypeBuilder<User> builder)
  {
    base.Configure(builder);

    builder.ToTable("user");
    builder.HasIndex(user => user.Email).IsUnique();

    builder.Property(user => user.CompanyId).IsRequired(false);
    builder.Property(user => user.Email).IsRequired();
    builder.Property(user => user.EmailVerified).HasDefaultValue(false);
    builder.Property(user => user.Role).HasDefaultValue("user");
    builder.Property(user => user.Name).IsRequired();
    builder.Property(user => user.Image).IsRequired(false);

    builder.HasMany(user => user.Accounts)
       .WithOne(account => account.User)
       .HasForeignKey(account => account.UserId)
       .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(user => user.Sessions)
       .WithOne(session => session.User)
       .HasForeignKey(session => session.UserId)
       .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(user => user.Companies)
       .WithOne(company => company.User)
       .HasForeignKey(company => company.UserId)
       .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(user => user.Tickets)
       .WithOne(ticket => ticket.User)
       .HasForeignKey(ticket => ticket.UserId)
       .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(user => user.Assigned)
       .WithOne(ticket => ticket.Assignee)
       .HasForeignKey(ticket => ticket.AssigneeId)
       .OnDelete(DeleteBehavior.SetNull);

    builder.HasMany(user => user.TicketHistories)
       .WithOne(ticketHistory => ticketHistory.User)
       .HasForeignKey(ticketHistory => ticketHistory.UserId)
       .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(user => user.TicketAttachments)
       .WithOne(ticketAttachment => ticketAttachment.User)
       .HasForeignKey(ticketAttachment => ticketAttachment.UploadedBy)
       .OnDelete(DeleteBehavior.SetNull);

    builder.HasMany(user => user.TicketNotifications)
       .WithOne(ticketNotification => ticketNotification.User)
       .HasForeignKey(ticketNotification => ticketNotification.UserId)
       .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(user => user.Comments)
       .WithOne(comment => comment.User)
       .HasForeignKey(comment => comment.UserId)
       .OnDelete(DeleteBehavior.Cascade);
  }
}
