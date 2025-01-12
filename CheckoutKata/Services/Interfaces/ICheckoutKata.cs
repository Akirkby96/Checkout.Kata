namespace Checkout.Kata.Services.Interfaces
{
    public interface ICheckoutKata
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void Scan(string item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetTotalPrice();
    }
}
