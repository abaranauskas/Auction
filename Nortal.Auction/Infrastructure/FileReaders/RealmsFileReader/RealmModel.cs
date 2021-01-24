namespace Nortal.Auction.Infrastructure.FileReaders.RealmsFileReader
{
    public class RealmModel
    {
        public RealmModel(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public string Name { get; }
        public string Slug { get; }
    }
}