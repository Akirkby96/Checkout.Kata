namespace Checkout.Kata.Models
{
    /// <summary>
    /// Class to hold pricing rules for items
    /// </summary>
    /// <param name="unitPrice"></param>
    /// <param name="specialQuantity"></param>
    /// <param name="specialPrice"></param>
    public class PricingRule(int unitPrice, int specialQuantity = 0, int specialPrice = 0)
    {
        public int UnitPrice { get; set; } = unitPrice;
        public int SpecialQuantity { get; set; } = specialQuantity;
        public int SpecialPrice { get; set; } = specialPrice;
    }
}
