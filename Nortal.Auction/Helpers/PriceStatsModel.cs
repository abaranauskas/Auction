using System;

namespace Nortal.Auction.Helpers
{
    public class PriceStatsModel
    {
        public PriceStatsModel(decimal maxPrice, decimal minPrice, decimal averagePrice, decimal medianPrice)
        {
            MaxPrice = maxPrice;
            MinPrice = minPrice;
            AveragePrice = Math.Round(averagePrice, 2);
            MedianPrice = Math.Round(medianPrice, 2);
        }

        public decimal MaxPrice { get; }
        public decimal MinPrice { get; }
        public decimal AveragePrice { get; }
        public decimal MedianPrice { get; }
    }
}