using CloudStorage.Entity;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Contexts
{
    public class MsDatabaseContext : DbContext
    {
        public MsDatabaseContext(DbContextOptions<MsDatabaseContext> option) : base(option) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
                .HasMany(document => document.AllowedUsers)
                .WithMany(user => user.AccessedDocuments);

            modelBuilder.Entity<Document>()
                .HasOne(document => document.Owner)
                .WithMany(user => user.CreatedDocuments)
                .HasForeignKey(document => document.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
