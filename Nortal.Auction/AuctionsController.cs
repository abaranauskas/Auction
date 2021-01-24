using Nortal.Auction.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nortal.Auction
{
    public class AuctionsController
    {
        private readonly AuctionContext _context;

        public AuctionsController(AuctionContext context)
        {
            _context = context;
        }

        public void GetItemDetails(IList<long> lists)
        {
            throw new NotImplementedException();
        }
    }
}
