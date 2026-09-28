using Microsoft.EntityFrameworkCore;
using TruppenApi.Models;

namespace TruppenApi.Data;

public class AppDbContext : DbContext
{ // Databaskontext för applikationen
// Denna är hanteringen av databasanslutning, innehåller DbSet för spelare samt konfig av enum som text i databasen istället för siffror.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Player> Players => Set<Player>(); // representering av tabellen för spelare i databasen

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Konfiguration av modellen vid skapande av databasen
    {
        // Enum som text i databasen istället för siffror - läsbart vid felsökning
        modelBuilder.Entity<Player>().Property(p => p.Position).HasConversion<string>();
        modelBuilder.Entity<Player>().Property(p => p.Status).HasConversion<string>();
    }
}