namespace Nortal.Auction.Infrastructure.FileReaders.ActionsFileReader
{
    public class AuctionModel
    {
        public AuctionModel(long id, long itemId, decimal buyout, string owner, string ownerRealm, int quantity)
        {
            Id = id;
            ItemId = itemId;
            Buyout = buyout;
            Owner = owner;
            OwnerRealm = ownerRealm;
            Quantity = quantity;
        }

        public long Id { get; }
        public long ItemId { get; }
        public decimal Buyout { get; }
        public int Quantity { get; }
        public string Owner { get; }
        public string OwnerRealm { get; }
    }
}