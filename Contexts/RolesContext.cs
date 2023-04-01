using AuthServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Contexts;

public class RolesContext : DbContext
{
    public RolesContext(DbContextOptions<RolesContext> options) : base(options)
    {
    }

    public DbSet<Roles> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sequential IDs
        modelBuilder.Entity<Roles>()
            .Property(r => r.Id)
            .HasDefaultValueSql("nextval('roles_id_seq')");

        modelBuilder.Entity<Roles>()
        .HasMany(r => r.Users)
        .WithMany(u => u.Roles);

        modelBuilder.Entity<Roles>()
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity(j => j.ToTable("roles_permissions"));
    }
}