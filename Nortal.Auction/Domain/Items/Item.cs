using System.Collections.Generic;
using System.Linq;

namespace Nortal.Auction.Domain.Items
{
    public class Item : Entity
    {
        private readonly List<Auctions.Auction> _auctions = new List<Auctions.Auction>();

        protected Item()
        {
        }

        public Item(long id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; }
        public virtual IReadOnlyList<Auctions.Auction> Auctions => _auctions.ToList();
    }
}
