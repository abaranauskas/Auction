using FluentAssertions;
using Nortal.Auction.Helpers;
using System.Collections.Generic;
using Xunit;

namespace Nortal.Auction.Tests.Helpers
{
    public class PriceStatsHelperTest
    {
        [Fact]
        public void ShouldGetCorrectPricesStatictics()
        {
            //Arrange
            List<decimal> priceList = new List<decimal> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var expectedAverage = 6;
            var expectedMedian = 6;
            var expectedMax = 11;
            var expectedMin = 1;

            //Act
            var result = PriceStatsHelper.GetItemPriceStats(priceList);

            //Assert
            result.AveragePrice.Should().Be(expectedAverage);
            result.MedianPrice.Should().Be(expectedMedian);
            result.MaxPrice.Should().Be(expectedMax);
            result.MinPrice.Should().Be(expectedMin);
        }
    }
}
