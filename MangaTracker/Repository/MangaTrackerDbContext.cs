using MangaTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaTracker.Repository
{
    public class MangaTrackerDbContext(DbContextOptions<MangaTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>()
                .HasMany(m => m.Creators)
                .WithMany(c => c.Mangas)
                .UsingEntity(t => t.ToTable("MangaCreator"));
        }
    }
}
