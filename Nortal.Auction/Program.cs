using Nortal.Auction.Data;
using Nortal.Auction.DataBaseLoands;
using Nortal.Auction.Infrastructure.Persistence;
using Nortal.Auction.Items;
using Nortal.Auction.Utils;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nortal.Auction
{
    class Program
    {
        private static ItemsController _itemsController;
        private static DataBaseLoadController _dataBaseLoadController;

        static async Task Main(string[] args)
        {           
            using var context = new AuctionContext();
            context.Database.EnsureCreated();
            _itemsController = new ItemsController(context);
            _dataBaseLoadController = new DataBaseLoadController(context);

            while (true)
            {
                PrintMenu();
                var result = int.TryParse(Console.ReadLine(), out var action);

                if (!result)
                    continue;

                switch (action)
                {
                    case 1:
                        LoadDataToDataBase();
                        break;
                    case 2:
                        await GetItemDetails();
                        break;
                    case 3:
                        await GetTenMostExpensiveItems();
                        break;
                    case 4:
                        await GetTenMostActiveOwners();
                        break;
                    case 0:
                        return;                    
                }
            }
        }

        private async static Task GetTenMostActiveOwners()
        {
            Console.WriteLine("Please wait. Loading...");
             var result = await _itemsController.GetTenMostActiveOwners();

            int iterator = 1;
            foreach (var item in result)
            {
                Console.WriteLine($"{iterator}) Owner: {item.Owner} | Sales quantity: {item.SalesQuantity} | Sales amount: {item.SalesAmount}");
                iterator++;
            }
        }

        private async static Task GetTenMostExpensiveItems()
        {
            Console.WriteLine("Please wait. Loading...");
            var result = await _itemsController.GetTenMostExpensiveItems();

            int iterator = 1;
            foreach (var item in result)
            {
                Console.WriteLine($"{iterator}) Item id: {item.Id} | Item name: {item.Name} | Price: {item.Price}");
                iterator++;
            }
        }

        private async static Task GetItemDetails()
        {
            Console.WriteLine("Please wait. Loading...");
            var result = _itemsController.GetItemDetails(ItemNames.Data.Select(x => x.Key).ToList());

            await foreach (var item in result)
            {
                Console.WriteLine($"Item id: {item.Id} | Item name: {item.Name} | Min price: {item.PriceStats.MinPrice} | Max price: {item.PriceStats.MaxPrice} | Average price: {item.PriceStats.AveragePrice} | Median price: {item.PriceStats.MedianPrice}");
            }
        }

        private static void LoadDataToDataBase()
        {

            Console.WriteLine("Please wait. Loading...");
            _dataBaseLoadController.SaveItemNames(ItemNames.Data);
            _dataBaseLoadController.SaveRealmsData(Path.Combine(Directory.GetCurrentDirectory(), "Data", "AuctionsData"));
            _dataBaseLoadController.SaveAuctionsData(Path.Combine(Directory.GetCurrentDirectory(), "Data", "AuctionsData"));
            Console.WriteLine("Data has been loaded.");
            Console.WriteLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select action:");
            Console.WriteLine("1) Upload data to data base(Long running process).");
            Console.WriteLine("2) Get Item details.");
            Console.WriteLine("3) Get 10 most expensive items in auction.");
            Console.WriteLine("4) Get 10 most active owners.");
            Console.WriteLine("0) Exit.");
        }
    }
}
