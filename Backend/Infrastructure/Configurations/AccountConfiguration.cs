using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountConfiguration : BaseConfiguration<Account>
{
  public override void Configure(EntityTypeBuilder<Account> builder)
  {
    base.Configure(builder);

    builder.ToTable("account");
    builder.HasIndex(account => account.UserId);

    builder.Property(account => account.UserId).IsRequired();
    builder.Property(account => account.ProviderId).IsRequired();
    builder.Property(account => account.AccountId).IsRequired();
    builder.Property(account => account.Password).IsRequired(false);
    builder.Property(account => account.Scope).IsRequired(false);
    builder.Property(account => account.IdToken).IsRequired(false);
    builder.Property(account => account.AccessToken).IsRequired(false);
    builder.Property(account => account.RefreshToken).IsRequired(false);
    builder.Property(account => account.AccessTokenExpiresAt).IsRequired(false);
    builder.Property(account => account.RefreshTokenExpiresAt).IsRequired(false);

    builder.HasOne(account => account.User)
           .WithMany(user => user.Accounts)
           .HasForeignKey(account => account.UserId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
