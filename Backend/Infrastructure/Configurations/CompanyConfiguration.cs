using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.ToTable("company");

    builder.HasKey(company => company.Id);
    builder.HasIndex(company => company.UserId);
    builder.HasIndex(company => company.Email).IsUnique();

    builder.Property(company => company.UserId).IsRequired();
    builder.Property(company => company.Name).IsRequired();
    builder.Property(company => company.Email).IsRequired();
    builder.Property(company => company.PhoneNumber).IsRequired();
    builder.Property(company => company.Address).IsRequired();
    builder.Property(company => company.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(company => company.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasOne(company => company.User)
           .WithMany(user => user.Companies)
           .HasForeignKey(company => company.UserId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
