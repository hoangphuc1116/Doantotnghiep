namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class PagedProductListVM
    {
        public IEnumerable<ProductVM> SanPhams { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
