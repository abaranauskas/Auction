using Nortal.Auction.Helpers;

namespace Nortal.Auction
{
    public class ItemDetailsOutputModel
    {      
        public ItemDetailsOutputModel(long id, string name, PriceStatsModel stats)
        {
            Id = id;
            Name = name;
            PriceStats = stats;
        }

        public long Id { get; }
        public string Name { get; }
        public PriceStatsModel PriceStats { get; }
    }
}