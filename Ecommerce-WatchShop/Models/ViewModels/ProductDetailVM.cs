namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class ProductDetailVM
    {
        public SanPham SanPham { get; set; } = null!;

        public List<SanPham> RelatedProducts { get; set; } = new List<SanPham>();
        public double ProductRating { get; set; }
        public int TotalRating { get; set; }
        public List<ProductCommentVM> Comments { get; set; } = new List<ProductCommentVM>();

    }
}
