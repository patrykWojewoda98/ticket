using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddInfrastructure();
using var provider = services.BuildServiceProvider();

var dbContext = provider.GetRequiredService<DatabaseContext>();
var passwordHasher = provider.GetRequiredService<IPasswordHasher>();
var userRepository = provider.GetRequiredService<IUserRepository>();

Console.WriteLine("Starting login block/unblock test...");

// cleanup any previous test user
var existingUser = await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == "test-login-block@example.com");
if (existingUser != null)
{
    dbContext.Set<User>().Remove(existingUser);
    await dbContext.SaveChangesAsync();
}

var password = "correct-password";
var passwordHash = await passwordHasher.HashAsync(password);
var user = new User
{
    Email = "test-login-block@example.com",
    Name = "Login Block Test",
    Password = passwordHash,
    Role = "user",
    FailedLoginAttempts = 0,
    IsBlocked = false
};

await dbContext.Set<User>().AddAsync(user);
await dbContext.SaveChangesAsync();
Console.WriteLine($"Created test user with ID {user.Id}");

for (var i = 1; i <= 5; i++)
{
    var isValid = await passwordHasher.VerifyAsync("wrong-password", user.Password);
    if (!isValid)
    {
        await userRepository.IncrementFailedAttemptsAsync(user.Id);
    }
    var fresh = await dbContext.Set<User>().FirstAsync(u => u.Id == user.Id);
    Console.WriteLine($"Attempt {i}: FailedLoginAttempts={fresh.FailedLoginAttempts}, IsBlocked={fresh.IsBlocked}");
}

var blockedUser = await dbContext.Set<User>().FirstAsync(u => u.Id == user.Id);
if (!blockedUser.IsBlocked || blockedUser.FailedLoginAttempts < 5)
{
    Console.WriteLine("FAILED: expected user to be blocked after 5 failed attempts.");
    return;
}
Console.WriteLine("PASS: User blocked after 5 failed attempts.");

var blockedEntry = await dbContext.Set<BlockedUser>().FirstOrDefaultAsync(b => b.UserId == user.Id && b.UnblockedAt == null);
if (blockedEntry == null)
{
    Console.WriteLine("FAILED: expected blocked user record to exist.");
    return;
}
Console.WriteLine($"Blocked record found: Attempts={blockedEntry.Attempts}, Reason={blockedEntry.Reason}");

await userRepository.UnblockUserAsync(user.Id, adminId: 1);
var unblocked = await dbContext.Set<User>().FirstAsync(u => u.Id == user.Id);
if (unblocked.IsBlocked || unblocked.FailedLoginAttempts != 0)
{
    Console.WriteLine("FAILED: expected user to be unblocked and failed attempts reset.");
    return;
}
Console.WriteLine("PASS: User was unblocked and failed attempts reset.");

var blockedEntryUpdated = await dbContext.Set<BlockedUser>().FirstOrDefaultAsync(b => b.UserId == user.Id && b.UnblockedAt != null);
if (blockedEntryUpdated == null)
{
    Console.WriteLine("FAILED: expected blocked entry to be updated with unblocked timestamp.");
    return;
}
Console.WriteLine($"Blocked entry unblocked by admin {blockedEntryUpdated.UnblockedByAdminId} at {blockedEntryUpdated.UnblockedAt}");
