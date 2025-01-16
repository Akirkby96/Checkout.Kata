namespace Checkout.Kata.Services.Interfaces
{
    public interface ICheckoutKata
    {
        /// <summary>
        /// Scans an item and adds it to the checkout
        /// </summary>
        /// <param name="item"></param>
        void Scan(string item);

        /// <summary>
        /// Calculates the total price of all scanned items
        /// </summary>
        /// <returns>Total price</returns>
        int GetTotalPrice();
    }
}
