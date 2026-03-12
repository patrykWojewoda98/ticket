using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ICompanyRepository
{
  Task<List<Company>> GetAllAsync();
  Task<Company?> GetByIdAsync(string id);
  Task<Company> CreateAsync(Company company);
  Task<Company> UpdateAsync(string id, Company company);
  Task DeleteAsync(string id);
}
