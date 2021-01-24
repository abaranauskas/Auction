using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nortal.Auction.Domain.Items;
using Nortal.Auction.Domain.Realms;
using Nortal.Auction.Utils;

namespace Nortal.Auction.Infrastructure.Persistence
{
    public class AuctionContext : DbContext
    {
        public DbSet<Realm> Realms { get; protected set; }
        public DbSet<Domain.Auctions.Auction> Auctions { get; protected set; }
        public DbSet<Item> Items { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            if (Settings.IsEfLogsEnabled())
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
           
            optionsBuilder
                .UseSqlServer(Settings.GetConnectionString())
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Realm>(x =>
            {
                x.ToTable("Realms").HasKey(k => k.Id);
                x.Property(p => p.Id)
                    .HasColumnName("RealmId")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                x.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();
                x.Property(p => p.Slug)
                    .HasMaxLength(50)
                    .IsRequired();
                x.HasMany(p => p.Auctions)
                   .WithOne(p => p.OwnerRealm)
                   .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Domain.Auctions.Auction>(x =>
            {
                x.ToTable("Auctions").HasKey(k => k.Id);
                x.Property(p => p.Id)
                    .HasColumnName("AuctionId")
                    .ValueGeneratedNever()
                    .IsRequired();
                x.Property(p => p.Owner)
                    .HasMaxLength(50)
                    .IsRequired();
                x.Property(p => p.Price)
                    .IsRequired();
                x.Property(p => p.Buyout)
                    .IsRequired();
                x.Property(p => p.Quantity)
                    .IsRequired();
                x.HasOne(p => p.OwnerRealm)
                   .WithMany(p => p.Auctions)
                   .IsRequired();
                x.HasOne(p => p.Item)
                .WithMany(x => x.Auctions)
                .IsRequired();
            });

            modelBuilder.Entity<Item>(x =>
            {
                x.ToTable("Items").HasKey(k => k.Id);
                x.Property(p => p.Id)
                    .HasColumnName("ItemId")
                    .ValueGeneratedNever()
                    .IsRequired();
                x.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                x.HasMany(p => p.Auctions)
                    .WithOne(p => p.Item)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public void ResetDataBase()
        {
            Database.EnsureDeleted();
            Database.Migrate();
        }
    }
}
