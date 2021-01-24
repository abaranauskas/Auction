using Nortal.Auction.Data;
using Nortal.Auction.Domain.Auctions;
using Nortal.Auction.Domain.Items;
using Nortal.Auction.Domain.Realms;
using Nortal.Auction.Infrastructure.FileReaders.ActionsFileReader;
using Nortal.Auction.Infrastructure.FileReaders.RealmsFileReader;
using Nortal.Auction.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Nortal.Auction
{
    public class DataBaseLoadController
    {
        private readonly AuctionContext _auctionContext;
        private readonly RealmRepository _realmRepository;
        private readonly AuctionRepository _auctionRepository;
        private readonly ItemRepository _itemRepository;

        public DataBaseLoadController(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
            _realmRepository = new RealmRepository(_auctionContext);
            _auctionRepository = new AuctionRepository(_auctionContext);
            _itemRepository = new ItemRepository(auctionContext);
        }

        public void SaveItemNames(IDictionary<long, string> itemNames)
        {
            var items = itemNames.Select(x => new Item(x.Key, x.Value)).ToList();
            _itemRepository.Save(items);
            _auctionContext.SaveChanges();
        }

        public void SaveRealmsData(string path)
        {
            var raelmsBatches = RealmReader.Read(path);

            foreach (var batch in raelmsBatches)
            {
                _realmRepository.SaveBulk(batch.Select(x => new Realm(x.Name, x.Slug)).ToList());
                _auctionContext.SaveChanges();
            }
        }

        public void SaveAuctionsData(string path)
        {
            var auctionBatches = AuctionReader.Read(path);

            //TODO: this could be taken from item repo
            var items = _auctionContext.Items.ToList();

            foreach (var batch in auctionBatches)
            {
                //Fintering out auction records that itemsid is not present in ItemNames dictionary 
                var fiteredBatch = batch.Where(x => ItemNames.Data.ContainsKey(x.ItemId));

                var realms = _realmRepository.GetByNames(fiteredBatch.Select(x => x.OwnerRealm).ToList());

                _auctionRepository.SaveBulk(fiteredBatch.Select(x =>
                    new Domain.Auctions.Auction(x.Id, x.Owner, x.Buyout, x.Buyout / x.Quantity, x.Quantity,
                        items.Single(y => y.Id == x.ItemId), realms.Single(y => y.Name == x.OwnerRealm)))
                    .ToList());
                _auctionContext.SaveChanges();
            }
        }
    }
}
