using Nortal.Auction.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nortal.Auction.Domain.Realms
{
    public class RealmRepository
    {
        private AuctionContext _auctionContext;

        public RealmRepository(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
        }

        public void SaveBulk(IList<Realm> realms)
        {
            _auctionContext.Realms.AddRange(realms);
        }

        public IReadOnlyList<Realm> GetByNames(IList<string> names)
        {
            return _auctionContext.Realms.Where(x => names.Contains(x.Name)).ToList();
        }
    }
}
