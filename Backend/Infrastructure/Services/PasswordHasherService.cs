using System;
using System.Security.Cryptography;
using System.Text;
using Domain.Abstractions;
using Konscious.Security.Cryptography;

namespace Infrastructure.Services;

public class PasswordHasherService : IPasswordHasher
{
  private const int DegreeOfParallelism = 8;
  private const int MemorySize = 65536;
  private const int Iterations = 4;
  private const int SaltSize = 16;
  private const int HashSize = 32;

  public async Task<string> HashAsync(string password, CancellationToken cancellationToken = default)
  {
    var salt = RandomNumberGenerator.GetBytes(SaltSize);
    var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
    {
      Salt = salt,
      DegreeOfParallelism = DegreeOfParallelism,
      Iterations = Iterations,
      MemorySize = MemorySize
    };

    return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(argon2.GetBytes(HashSize))}";
  }

  public async Task<bool> VerifyAsync(string password, string passwordHash, CancellationToken cancellationToken = default)
  {
    var salt = Convert.FromBase64String(passwordHash.Split('.')[0]);
    var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
    {
      Salt = salt,
      DegreeOfParallelism = DegreeOfParallelism,
      Iterations = Iterations,
      MemorySize = MemorySize
    };

    return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(passwordHash.Split('.')[1]), argon2.GetBytes(HashSize));
  }
}
