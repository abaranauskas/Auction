namespace Nortal.Auction.Items.Dto
{
    public class MostExpensiveItemModel
    {
        public MostExpensiveItemModel(long id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public long Id { get; }
        public string Name { get; }
        public decimal Price { get; set; }
    }
}