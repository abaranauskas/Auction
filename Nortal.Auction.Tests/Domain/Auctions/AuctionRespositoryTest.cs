using FluentAssertions;
using Nortal.Auction.Domain.Auctions;
using Nortal.Auction.Domain.Items;
using Nortal.Auction.Domain.Realms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Nortal.Auction.Tests.Domain.Auctions
{
    public class AuctionRespositoryTest
    {
        [Fact]
        public async Task ShouldGetTenMostActiveOwners()
        {
            //Arrange
            using var context = new FaceAuctionContext();
            SeedData(context);
            var repository = new AuctionRepository(context);

            //Act
            var tenMostActiveOwners = await repository.GetTenMostActiveOwners();

            //Assert
            tenMostActiveOwners.Count.Should().Be(10);
            tenMostActiveOwners.First().Owner.Should().Be("Test11");
            tenMostActiveOwners.First().SalesAmount.Should().Be(444);
            tenMostActiveOwners.First().SalesQuantity.Should().Be(11111);
            tenMostActiveOwners.Last().Owner.Should().Be("Test6");
        }

        private static void SeedData(FaceAuctionContext context)
        {
            var realm = new Realm("Test realm name", "TestSlug");
            var item = new Item(1, "Test item name");

            var auctions = new List<Auction.Domain.Auctions.Auction>
            {
                new Auction.Domain.Auctions.Auction(1, "Test1", 2, 2,1,item, realm),
                new Auction.Domain.Auctions.Auction(2, "Test2", 6, 2,4,item, realm),
                new Auction.Domain.Auctions.Auction(3, "Test3", 6, 2,5,item, realm),
                new Auction.Domain.Auctions.Auction(4, "Test4", 6, 2,6,item, realm),
                new Auction.Domain.Auctions.Auction(5, "Test5", 600, 2,7,item, realm),
                new Auction.Domain.Auctions.Auction(6, "Test6", 4, 2,2,item, realm),
                new Auction.Domain.Auctions.Auction(7, "Test7", 688, 2,9,item, realm),
                new Auction.Domain.Auctions.Auction(8, "Test8", 677, 2,10,item, realm),
                new Auction.Domain.Auctions.Auction(9, "Test9", 6666, 2,111,item, realm),
                new Auction.Domain.Auctions.Auction(10, "Test10", 6555, 2,1111,item, realm),
                new Auction.Domain.Auctions.Auction(11, "Test11", 444, 2,11111,item, realm),
            };

            context.Auctions.AddRange(auctions);
            context.SaveChanges();
        }
    }
}
