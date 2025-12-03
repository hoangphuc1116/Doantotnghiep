using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models
{
    public class TokenKhoiPhucMatKhau
    {
        [Key]
        [Display(Name = "Mã định danh")]
        public int MaDinhDanh { get; set; }

        [Required(ErrorMessage = "Mã tài khoản là bắt buộc.")]
        [Display(Name = "Mã tài khoản")]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Mã token là bắt buộc.")]
        [Display(Name = "Mã token")]
        public string MaToken { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc.")]
        [Display(Name = "Ngày hết hạn")]
        public DateTime NgayHetHan { get; set; }

        [ForeignKey("MaTaiKhoan")]
        [Display(Name = "Tài khoản")]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}