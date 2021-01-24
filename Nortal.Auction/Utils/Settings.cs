using Microsoft.Extensions.Configuration;
using Nortal.Auction.Infrastructure.Persistence;
using System;
using System.IO;

namespace Nortal.Auction.Utils
{
    public static  class Settings
    {
        private static IConfigurationRoot _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        public static string GetConnectionString()
        {      
            return _configuration["ConnectionString"];
        }

        public static bool IsEfLogsEnabled()
        {
            return bool.Parse(_configuration["EFLogsEnabled"]);
        }
    }
}
