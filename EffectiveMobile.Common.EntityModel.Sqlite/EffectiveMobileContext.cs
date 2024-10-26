using EffectiveMobile.Common.EntityModel.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;

namespace EffectiveMobile.Common.EntityModel.Sqlite;

public class EffectiveMobileContext : DbContext
{
    public EffectiveMobileContext()
    {
    }

    public EffectiveMobileContext(DbContextOptions<EffectiveMobileContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite();

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}