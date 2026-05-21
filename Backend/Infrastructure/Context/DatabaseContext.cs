using System;
<<<<<<< HEAD
using System.IO;
using System.Text.Json;
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DatabaseContext : DbContext
{
  public DbSet<Comment> Comments { get; set; }
  public DbSet<Company> Companies { get; set; }
  public DbSet<Ticket> Tickets { get; set; }
  public DbSet<TicketAttachment> TicketAttachments { get; set; }
  public DbSet<TicketCategory> TicketCategories { get; set; }
  public DbSet<TicketHistory> TicketHistories { get; set; }
  public DbSet<TicketNotification> TicketNotifications { get; set; }
  public DbSet<TicketPriority> TicketPriorities { get; set; }
  public DbSet<TicketStatus> TicketStatuses { get; set; }
  public DbSet<User> Users { get; set; }
<<<<<<< HEAD
  public DbSet<BlockedUser> BlockedUsers { get; set; }

  public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
=======

  public DatabaseContext() { }
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      DotNetEnv.Env.TraversePath().Load();
      var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
<<<<<<< HEAD

      if (string.IsNullOrWhiteSpace(connectionString))
      {
        var projectDir = Directory.GetCurrentDirectory();
        var settingsPath = Path.Combine(projectDir, "appsettings.Development.json");

        if (!File.Exists(settingsPath))
          settingsPath = Path.Combine(projectDir, "appsettings.json");

        if (File.Exists(settingsPath))
        {
          var fileText = File.ReadAllText(settingsPath);
          using var doc = JsonDocument.Parse(fileText);

          if (doc.RootElement.TryGetProperty("ConnectionStrings", out var connSection)
            && connSection.ValueKind == JsonValueKind.Object
            && connSection.TryGetProperty("DefaultConnection", out var defaultConn)
            && defaultConn.ValueKind == JsonValueKind.String)
          {
            connectionString = defaultConn.GetString();
          }
        }
      }

      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new InvalidOperationException("Database connection string is not configured. Set ConnectionStrings:DefaultConnection in configuration or DB_CONNECTION_STRING environment variable.");
      }

=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
      optionsBuilder.UseSqlServer(connectionString);
    }
  }
}
