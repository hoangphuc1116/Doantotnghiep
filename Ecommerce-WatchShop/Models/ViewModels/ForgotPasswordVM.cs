using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }
    }
}