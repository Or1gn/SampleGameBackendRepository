using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.DatabaseHandler
{
    public class CoreDbContext : DbContext
    {
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<Enemy> Enemies { get; set; }
        public virtual DbSet<PlayerInfo> Players { get; set; }
        public virtual DbSet<StoreItem> StoreItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerInfo>()
                .HasOne(x => x.User)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PlayerInfo>()
                .HasMany(x => x.InventoryItems)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);
        }

    }
}
