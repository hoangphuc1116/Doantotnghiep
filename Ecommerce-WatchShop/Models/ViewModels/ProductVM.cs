using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        public string Image { get; set; } = null!;

        public string ProductName { get; set; } = string.Empty;

        public double Price { get; set; }

        public double? PromotionPrice { get; set; }

        public decimal? DiscountPercent { get; set; }

        public string ShortDescription { get; set; } = string.Empty;

        public double ProductRating { get; set; }

        public int TotalRating { get; set; }

        public string? Slug { get; set; }

        public int SoldQuantity { get; set; }

    }
}