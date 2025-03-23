using Microsoft.EntityFrameworkCore;

namespace TrackMate.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        }

        public DbSet<Collections> Collections { get; set; }
        public DbSet<CollectionItems> CollectionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly set CollectionId as the primary key for Collections
            modelBuilder.Entity<Collections>()
                .HasKey(c => c.CollectionID);

            // Explicitly set ItemID as the primary key for CollectionItems
            modelBuilder.Entity<CollectionItems>()
                .HasKey(ci => ci.ItemID);

            // You can also define a foreign key relationship if you want
            modelBuilder.Entity<CollectionItems>()
                .HasOne<Collections>()
                .WithMany()
                .HasForeignKey(ci => ci.CollectionID);
        }
    }
}
