using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
{
  public void Configure(EntityTypeBuilder<BlockedUser> builder)
  {
    builder.ToTable("BlockedUsers");
    builder.HasKey(b => b.Id);
    builder.Property(b => b.UserId).IsRequired();
    builder.Property(b => b.BlockedAt).IsRequired();
    builder.Property(b => b.Attempts).IsRequired();
    builder.Property(b => b.Reason).HasMaxLength(256).IsRequired(false);
    builder.Property(b => b.UnblockedAt).IsRequired(false);
    builder.Property(b => b.UnblockedByAdminId).IsRequired(false);
  }
}
