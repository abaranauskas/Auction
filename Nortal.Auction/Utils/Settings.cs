using Microsoft.Extensions.Configuration;
using Nortal.Auction.Infrastructure.Persistence;
using System.IO;

namespace Nortal.Auction.Utils
{
    public class Settings
    {
        public string ConnectionString { get; }

        private Settings()
        {
            ConnectionString = GetConnectionString();
            //AuctionContext.ResetDataBase();
        }

        //public AuctionContext AuctionContext { get; }

        public static Settings Init()
        {
            return new Settings();
        }

        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration["ConnectionString"];
        }
    }
}
