using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CompanyConfiguration : BaseConfiguration<Company>
{
  public override void Configure(EntityTypeBuilder<Company> builder)
  {
    base.Configure(builder);

    builder.ToTable("company");
    builder.HasIndex(company => company.UserId);
    builder.HasIndex(company => company.Email).IsUnique();

    builder.Property(company => company.UserId).IsRequired();
    builder.Property(company => company.Name).IsRequired();
    builder.Property(company => company.Email).IsRequired();
    builder.Property(company => company.PhoneNumber).IsRequired();
    builder.Property(company => company.Address).IsRequired();

    builder.HasOne(company => company.User)
           .WithMany(user => user.Companies)
           .HasForeignKey(company => company.UserId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
