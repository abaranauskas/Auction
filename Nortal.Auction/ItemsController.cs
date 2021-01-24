using Nortal.Auction.Domain.Auctions;
using Nortal.Auction.Domain.Items;
using Nortal.Auction.Helpers;
using Nortal.Auction.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nortal.Auction
{
    public class ItemsController
    {
        private readonly ItemRepository _itemRepository;
        private readonly AuctionRepository _auctionRepository;

        public ItemsController(AuctionContext auctionContext)
        {
            _itemRepository = new ItemRepository(auctionContext);
            _auctionRepository = new AuctionRepository(auctionContext);
        }

        public async IAsyncEnumerable<ItemDetailsOutputModel> GetItemDetails(IList<long> itemIds)
        {
            foreach (var id in itemIds)
            {
                var item = await _itemRepository.GetById(id);
                var prices = await _auctionRepository.GetPricesByItemId(id);
                var stats = PriceStatsHelper.GetItemPriceStats(prices);
                yield return new ItemDetailsOutputModel(item.Id, item.Name, stats);
            }
        }

        public async Task<IEnumerable<MostExpensiveItemModel>> GetTenMostExpensiveItems()
        {
            var auctions = await _auctionRepository.GetTenMostExpenciveAuctions();
            return auctions.Select(x => new MostExpensiveItemModel(x.ItemId, x.Name, x.Price));
        }

        public async Task<IReadOnlyList<MostActiveOwnerModel>> GetTenMostActiveOwners()
        {
            return await _auctionRepository.GetTenMostActiveOwners();
        }
    }
}
