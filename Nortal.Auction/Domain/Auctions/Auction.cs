using Nortal.Auction.Domain.Items;
using Nortal.Auction.Domain.Realms;

namespace Nortal.Auction.Domain.Auctions
{
    public class Auction : Entity
    {
        protected Auction()
        {
        }

        public Auction(long id, string owner, decimal buyout, decimal price, int quantity, Item item, Realm ownerRealm)
            : base(id)
        {
            Owner = owner;
            Buyout = buyout;
            Price = price;
            Quantity = quantity;
            Item = item;
            OwnerRealm = ownerRealm;
        }

        public string Owner { get; }
        public decimal Buyout { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public virtual Realm OwnerRealm { get; }
        public virtual Item Item { get; }
    }
}
