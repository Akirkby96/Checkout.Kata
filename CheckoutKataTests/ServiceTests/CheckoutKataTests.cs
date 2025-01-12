using Checkout.Kata.Models;
using Checkout.Kata.Services;

namespace Checkout.Kata.Tests.ServiceTests
{
    public class CheckoutKataTests
    {
        Dictionary<string, PricingRule> _standardPricingRules;

        public CheckoutKataTests()
        {
            // Set up standard pricing rules
            _standardPricingRules = new Dictionary<string, PricingRule>
            {
                { "A", new PricingRule(50, 3, 130) },
                { "B", new PricingRule(30, 2, 45) },
                { "C", new PricingRule(20) },
                { "D", new PricingRule(15) }
            };
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Scan_WithInvalidItem_ThrowsArgumentException(string item)
        {
            // Arrange
            var checkout = new CheckoutKata(_standardPricingRules);

            // Act
            var act = () => checkout.Scan(item);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Item cannot be null or empty*");
        }

        [Fact]
        public void GetTotalPrice_MixedItemsWithSpecials_ReturnsCorrectTotal()
        {
            // Arrange
            var checkout = new CheckoutKata(_standardPricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            var total = checkout.GetTotalPrice();

            // Assert - As's should be 130, plus 45 from b's is 175
            total.Should().Be(175);
        }

        [Fact]
        public void GetTotalPrice_ItemsWithPartialSpecial_ReturnsCorrectTotal()
        {
            // Arrange
            var checkout = new CheckoutKata(_standardPricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            var total = checkout.GetTotalPrice();

            // Assert
            total.Should().Be(180);
        }

        [Fact]
        public void GetTotalPrice_MultipleItems_NoSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var checkout = new CheckoutKata(_standardPricingRules);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            var total = checkout.GetTotalPrice();

            // Assert
            total.Should().Be(80);
        }

    }
}
