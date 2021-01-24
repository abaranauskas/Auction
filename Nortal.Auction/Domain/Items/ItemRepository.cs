using Nortal.Auction.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nortal.Auction.Domain.Items
{
    public class ItemRepository
    {
        private readonly AuctionContext _context;

        public ItemRepository(AuctionContext context)
        {
            _context = context;
        }

        public void Save(IList<Item> lists)
        {
            _context.Items.AddRange(lists);
        }

        public async Task<Item> GetById(long id)
        {
            return await _context.Items.FindAsync(id);
        }
    }
}
