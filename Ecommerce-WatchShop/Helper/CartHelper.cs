using Ecommerce_WatchShop.Helper;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public static class CartHelper
{
    private const string CART_KEY = "MYCART";

    public static List<CartRequest> GetCart(ISession session)
    {
        return session.Get<List<CartRequest>>(CART_KEY) ?? new List<CartRequest>();
    }

    public static void SetCart(ISession session, List<CartRequest> cart)
    {
        session.Set(CART_KEY, cart);
    }

    public static void ClearCart(ISession session)
    {
        session.Remove(CART_KEY);
    }
}
