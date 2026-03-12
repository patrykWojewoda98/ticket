using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
  private readonly DatabaseContext _context;

  public CompanyRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Company>> GetAllAsync()
  {
    return await _context.Companies.ToListAsync();
  }

  public async Task<Company?> GetByIdAsync(string id)
  {
    return await _context.Companies.FindAsync(id);
  }

  public async Task<Company> CreateAsync(Company company)
  {
    await _context.Companies.AddAsync(company);
    await _context.SaveChangesAsync();
    return company;
  }

  public async Task<Company> UpdateAsync(string id, Company company)
  {
    var existingCompany = await _context.Companies.FindAsync(id);
    if (existingCompany != null)
    {
      existingCompany.UserId = company.UserId;
      existingCompany.Name = company.Name;
      existingCompany.Email = company.Email;
      existingCompany.PhoneNumber = company.PhoneNumber;
      existingCompany.Address = company.Address;
      existingCompany.UpdatedAt = company.UpdatedAt;

      _context.Companies.Update(existingCompany);
      await _context.SaveChangesAsync();
      return existingCompany;
    }
    return company;
  }

  public async Task DeleteAsync(string id)
  {
    var existingCompany = await _context.Companies.FindAsync(id);
    if (existingCompany != null)
    {
      _context.Companies.Remove(existingCompany);
      await _context.SaveChangesAsync();
    }
  }
}
