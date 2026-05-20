using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CommentConfiguration : BaseConfiguration<Comment>
{
  public override void Configure(EntityTypeBuilder<Comment> builder)
  {
    base.Configure(builder);

    builder.ToTable("comment");
    builder.HasIndex(comment => comment.UserId);

    builder.Property(comment => comment.UserId).IsRequired();
    builder.Property(comment => comment.TicketId).IsRequired();
    builder.Property(comment => comment.Content)
           .IsRequired()
           .HasColumnType("text");

    builder.HasOne(comment => comment.User)
           .WithMany(user => user.Comments)
           .HasForeignKey(comment => comment.UserId)
           .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(comment => comment.Ticket)
           .WithMany(ticket => ticket.Comments)
           .HasForeignKey(comment => comment.TicketId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
