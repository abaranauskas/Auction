using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nortal.Auction.Infrastructure.FileReaders.ActionsFileReader
{
    public class AuctionReader
    {
        public static IEnumerable<IList<AuctionModel>> Read(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                var batch = new List<AuctionModel>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject && reader.Path.StartsWith("auctions"))
                    {
                        JObject obj = JObject.Load(reader);

                        var id = long.Parse(obj["auc"].ToString());
                        var itemId = long.Parse(obj["item"].ToString());
                        var buyout = decimal.Parse(obj["buyout"].ToString());
                        var ownerRealm = obj["ownerRealm"].ToString();
                        var owner = obj["owner"].ToString();
                        var quantity = int.Parse(obj["quantity"].ToString());

                        batch.Add(new AuctionModel(id, itemId, buyout, owner, ownerRealm, quantity));

                        if (batch.Count == 250)
                        {
                            yield return batch;
                            batch.Clear();
                        }
                    }
                }

                if (batch.Any())
                {
                    yield return batch;
                    batch.Clear();
                }
            }
        }
    }
}
