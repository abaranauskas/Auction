using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nortal.Auction.Infrastructure.FileReaders.RealmsFileReader
{
    public class RealmReader
    {
        public static IEnumerable<IList<RealmModel>> Read(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                var batch = new List<RealmModel>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject && reader.Path.StartsWith("realms"))
                    {
                        JObject obj = JObject.Load(reader);

                        var name = obj["name"].ToString();
                        var slug = obj["slug"].ToString();

                        batch.Add(new RealmModel(name, slug));

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
