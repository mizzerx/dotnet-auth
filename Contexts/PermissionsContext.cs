using AuthServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Contexts;

public class PermissionsContext : DbContext
{
    public PermissionsContext(DbContextOptions<PermissionsContext> options) : base(options)
    {
    }

    public DbSet<Permissions> Permissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sequential IDs
        modelBuilder.Entity<Permissions>()
            .Property(p => p.Id)
            .HasDefaultValueSql("nextval('permissions_id_seq')");

        modelBuilder.Entity<Permissions>()
            .Property(p => p.Actions)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Permissions>()
            .Property(p => p.Resources)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Permissions>()
            .HasMany(p => p.Roles)
            .WithMany(r => r.Permissions);
    }
}