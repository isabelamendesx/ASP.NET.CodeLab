using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly DiscountOptions _discountOptions;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IOptionsMonitor<DiscountOptions> discountOptions)
        {
            _logger = logger;
            _discountOptions = discountOptions.CurrentValue;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var applySeasonalDiscount = _discountOptions.ApplySeasonalDiscount;
            var message = applySeasonalDiscount ? "I'm Alexa with Seasonal Discount applied" : "I'm Alexa without Seasonal Discount applied";
            var price = applySeasonalDiscount ? 320 : 400;

            var shoppingCartItem = new ShoppingCartItem
            {
                ProductName = "Echo Dot Alexa",
                Price = price,
                SeasonalDiscount = message
            };

            return Ok(new List<ShoppingCartItem> { shoppingCartItem });
        }
    }
}
