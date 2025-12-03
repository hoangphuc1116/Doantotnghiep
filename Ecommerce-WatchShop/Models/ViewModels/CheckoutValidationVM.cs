using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class CheckoutValidationVM
    {
        public CheckoutVM CheckoutVM { get; set; } = new CheckoutVM();
        public List<CartRequest> CartRequest { get; set; } = new List<CartRequest>();

        // Thêm các thuộc tính địa chỉ
        public int? MaTinh { get; set; }
        public int? MaHuyen { get; set; }
        public int? MaXa { get; set; }
    }
}