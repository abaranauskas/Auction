using Microsoft.EntityFrameworkCore;
using Nortal.Auction.Infrastructure.Persistence;


namespace Nortal.Auction.Tests.Domain
{
    public class FaceAuctionContext : AuctionContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder
                .UseInMemoryDatabase("TestAuctionDB")
                .UseLazyLoadingProxies();
        }
    }
}
