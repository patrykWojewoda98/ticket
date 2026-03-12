using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
  public void Configure(EntityTypeBuilder<Comment> builder)
  {
    builder.ToTable("comment");

    builder.HasKey(comment => comment.Id);
    builder.HasIndex(comment => comment.UserId);

    builder.Property(comment => comment.UserId).IsRequired();
    builder.Property(comment => comment.TicketId).IsRequired();
    builder.Property(comment => comment.Content).IsRequired().HasColumnType("text");
    builder.Property(comment => comment.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(comment => comment.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

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
