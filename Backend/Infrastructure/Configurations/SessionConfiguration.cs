using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SessionConfiguration : BaseConfiguration<Session>
{
  public override void Configure(EntityTypeBuilder<Session> builder)
  {
    base.Configure(builder);

    builder.ToTable("session");
    builder.HasIndex(session => session.UserId);
    builder.HasIndex(session => session.Token).IsUnique();

    builder.Property(session => session.UserId).IsRequired();
    builder.Property(session => session.Token).IsRequired();
    builder.Property(session => session.ExpiresAt).IsRequired();
    builder.Property(session => session.IpAddress).IsRequired(false);
    builder.Property(session => session.UserAgent).IsRequired(false);

    builder.HasOne(session => session.User)
           .WithMany(user => user.Sessions)
           .HasForeignKey(session => session.UserId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
