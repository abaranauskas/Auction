namespace Nortal.Auction
{
    public class MostActiveOwnerModel
    {
       
        public MostActiveOwnerModel(string owner, int salesQuantity, decimal salesAmount)
        {
            Owner = owner;
            SalesQuantity = salesQuantity;
            SalesAmount = salesAmount;
        }

        public string Owner { get; }
        public int SalesQuantity { get;  }
        public decimal SalesAmount { get;  }
    }
}
