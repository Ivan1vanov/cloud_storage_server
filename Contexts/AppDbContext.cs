using CloudStorage.Entity;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Contexts
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> option): base(option) {}

        public DbSet<User> Users { get; set; }

        public DbSet<Dokument> Dokuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

        modelBuilder.Entity<Dokument>()
            .HasMany(dokument => dokument.AllowedUsers)
            .WithMany(user => user.AccessedDokuments);

        modelBuilder.Entity<Dokument>()
            .HasOne(dokument => dokument.Owner)
            .WithMany(user => user.CreatedDokuments)
            .HasForeignKey(dokument => dokument.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}