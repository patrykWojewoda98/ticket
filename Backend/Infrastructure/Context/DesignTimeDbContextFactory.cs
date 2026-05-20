using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
  public DatabaseContext CreateDbContext(string[] args)
  {
    // Try environment variable first
    DotNetEnv.Env.TraversePath().Load();
    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

    // Try to read Api appsettings (useful when running tools with -s Backend/Api)
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      try
      {
        var projectDir = Directory.GetCurrentDirectory();
        // Expect structure: Backend/Infrastructure -> Backend/Api
        var apiSettingsPath = Path.Combine(projectDir, "..", "Api", "appsettings.Development.json");
        if (!File.Exists(apiSettingsPath))
          apiSettingsPath = Path.Combine(projectDir, "..", "Api", "appsettings.json");

        if (File.Exists(apiSettingsPath))
        {
          var json = File.ReadAllText(apiSettingsPath);
          using var doc = System.Text.Json.JsonDocument.Parse(json);
          if (doc.RootElement.TryGetProperty("ConnectionStrings", out var connSection) && connSection.ValueKind == System.Text.Json.JsonValueKind.Object)
          {
            if (connSection.TryGetProperty("DefaultConnection", out var defaultConn) && defaultConn.ValueKind == System.Text.Json.JsonValueKind.String)
              connectionString = defaultConn.GetString();
          }
        }
      }
      catch { /* ignore and fall through to error below */ }
    }

    if (string.IsNullOrWhiteSpace(connectionString))
    {
      // Fall back to LocalDB for design-time operations so EF tools can run locally.
      connectionString = "Server=(localdb)\\mssqllocaldb;Database=ProjektTicketDb;Trusted_Connection=True;MultipleActiveResultSets=true";
    }

    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
    optionsBuilder.UseSqlServer(connectionString);
    return new DatabaseContext(optionsBuilder.Options);
  }
}
