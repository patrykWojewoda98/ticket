using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class BackendContext : DbContext
{
  public DbSet<Account> Accounts { get; set; }
  public DbSet<Comment> Comments { get; set; }
  public DbSet<Company> Companies { get; set; }
  public DbSet<Session> Sessions { get; set; }
  public DbSet<Ticket> Tickets { get; set; }
  public DbSet<TicketAttachment> TicketAttachments { get; set; }
  public DbSet<TicketCategory> TicketCategories { get; set; }
  public DbSet<TicketHistory> TicketHistories { get; set; }
  public DbSet<TicketNotification> TicketNotifications { get; set; }
  public DbSet<TicketPriority> TicketPriorities { get; set; }
  public DbSet<TicketStatus> TicketStatuses { get; set; }
  public DbSet<User> Users { get; set; }

  public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}
