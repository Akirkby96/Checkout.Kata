using Checkout.Kata.Models;
using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services
{
    /// <summary>
    /// Constructor for CheckoutKata
    /// </summary>
    /// <param name="pricingRules"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public class CheckoutKata(Dictionary<string, PricingRule> pricingRules) : ICheckoutKata
    {

        // Stores the pricing rules for each unique item
        private readonly Dictionary<string, PricingRule> _pricingRules = pricingRules;

        // Stores how many of each item has been scanned
        private readonly Dictionary<string, int> _scannedItems = new Dictionary<string, int>();


        /// <summary>
        /// Scans an item and increments the count of that item
        /// </summary>
        /// <param name="item"></param>
        public void Scan(string item)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentException("Item cannot be null or empty", nameof(item));

            if (!_pricingRules.ContainsKey(item))
                throw new ArgumentException($"No pricing rule found for item {item}", nameof(item));

            // Initialize counter for this item if not present
            if (!_scannedItems.ContainsKey(item))
                _scannedItems[item] = 0;

            // Increment the count of this item
            _scannedItems[item]++;
        }

        /// <summary>
        /// Gets the total price of the scanned items, taking into account any special offers
        /// </summary>
        /// <returns></returns>
        public int GetTotalPrice()
        {
            int total = 0;

            // Calculate price for each type of item that was scanned
            foreach (var item in _scannedItems)
            {
                var priceRule = _pricingRules[item.Key];
                var quantity = item.Value;

                // Check if this item has a special offer
                if (priceRule.SpecialQuantity > 0)
                {
                    // Calculate how many sets of items qualify for special price
                    var specialOfferCount = quantity / priceRule.SpecialQuantity;

                    // Calculate how many items don't make a complete set
                    var remainingItems = quantity % priceRule.SpecialQuantity;

                    // Add special offer price for complete sets
                    total = total + (specialOfferCount * priceRule.SpecialPrice);

                    // Add regular price for remaining items
                    total = total + (remainingItems * priceRule.UnitPrice);
                }
                else
                {
                    // No special offer, just multiply quantity by unit price
                    total = total + quantity * priceRule.UnitPrice;
                }
            }

            return total;
        }
    }
}

