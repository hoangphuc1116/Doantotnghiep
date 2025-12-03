using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_WatchShop.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = CartHelper.GetCart(HttpContext.Session);
            return View("Default", new CartVM{
                Quantity = cart.Sum(p => p.Quantity),
                Total = cart.Sum(p => p.Total)
            });
        }
    }
}
