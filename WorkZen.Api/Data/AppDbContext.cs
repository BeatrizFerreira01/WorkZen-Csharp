using Microsoft.EntityFrameworkCore;
using WorkZen.Api.Entities;

namespace WorkZen.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Meditation> Meditations => Set<Meditation>();
    public DbSet<Session> Sessions => Set<Session>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacionamento 1:N
        modelBuilder.Entity<Meditation>()
            .HasMany(m => m.Sessions)
            .WithOne(s => s.Meditation!)
            .HasForeignKey(s => s.MeditationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}