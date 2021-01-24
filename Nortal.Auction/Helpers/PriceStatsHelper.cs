using System;
using System.Collections.Generic;
using System.Linq;

namespace Nortal.Auction.Helpers
{
    public static class PriceStatsHelper
    {
        public static PriceStatsModel GetItemPriceStats(IReadOnlyList<decimal> prices)
        {          
            return new PriceStatsModel(prices.Max(), prices.Min(), prices.Average(), GetMedian(prices));
        }

        private static decimal GetMedian(IReadOnlyList<decimal> prices)
        {
            var tempArray = prices.ToArray();
            var count = tempArray.Length;

            Array.Sort(tempArray);

            decimal medianValue = 0;

            if (count % 2 == 0)
            {
                var middleElement1 = tempArray[(count / 2) - 1];
                var middleElement2 = tempArray[(count / 2)];
                medianValue = (middleElement1 + middleElement2) / 2;
            }
            else
            {
                medianValue = tempArray[(count / 2)];
            }

            return medianValue;
        }
    }
}
