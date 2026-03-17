using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : Base
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(entity => entity.Id);

    builder.Property(entity => entity.CreatedAt)
           .HasDefaultValueSql("(UTC_TIMESTAMP())")
           .IsRequired();

    builder.Property(entity => entity.UpdatedAt)
           .HasDefaultValueSql("(UTC_TIMESTAMP())")
           .IsRequired();
  }
}
