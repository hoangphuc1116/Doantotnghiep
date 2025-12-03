using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.")]
        public string? ConfirmPassword { get; set; }

        public string? Token { get; set; }
    }
}