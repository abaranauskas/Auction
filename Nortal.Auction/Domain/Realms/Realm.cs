using System.Collections.Generic;
using System.Linq;

namespace Nortal.Auction.Domain.Realms
{
    public class Realm : Entity
    {
        protected Realm()
        {
        }

        private readonly List<Auctions.Auction> _auctions = new List<Auctions.Auction>();

        public Realm(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public string Name { get; }
        public string Slug { get; }
        public virtual IReadOnlyList<Auctions.Auction> Auctions => _auctions.ToList();
    }
}
