using Microsoft.EntityFrameworkCore;
using Nortal.Auction.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nortal.Auction.Domain.Auctions
{
    public class AuctionRepository
    {
        private AuctionContext _auctionContext;

        public AuctionRepository(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
        }

        public void SaveBulk(IList<Auction> auctions)
        {
            _auctionContext.Auctions.AddRange(auctions);
        }

        public async Task<IReadOnlyList<decimal>> GetPricesByItemId(long itemId)
        {
            return await _auctionContext.Auctions
                .Where(x => x.Item.Id == itemId)
                .Select(x => x.Price)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<(long ItemId, decimal Price, string Name)>> GetTenMostExpenciveAuctions()
        {
            return (await _auctionContext.Auctions
                .Select(x => new { x.Item.Id, x.Price, x.Item.Name })
                .Distinct()
                .OrderByDescending(x => x.Price)
                .Take(10)
                .ToListAsync())
                .Select(x => (x.Id, x.Price, x.Name))
                .ToList();
        }

        public async Task<IReadOnlyList<MostActiveOwnerModel>> GetTenMostActiveOwners()
        {
            return (await _auctionContext.Auctions
                .GroupBy(x => x.Owner)
                .Select(group => new
                {
                    Owner = group.Key,
                    SalesQuantity = group.ToList().Sum(x => x.Quantity),
                    SalesAmount = group.ToList().Sum(x => x.Buyout)
                })
                .OrderByDescending(x => x.SalesQuantity)
                .ThenByDescending(x => x.SalesAmount)
                .Take(10)
                .ToListAsync())
                .Select(x => new MostActiveOwnerModel(x.Owner, x.SalesQuantity, x.SalesAmount))
                .ToList();
        }
    }
}
