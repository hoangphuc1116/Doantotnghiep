using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class TaiKhoan
{
    [Key]
    public int MaTaiKhoan { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string? TenDangNhap { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string? MatKhau { get; set; }

    public int? MaVaiTro { get; set; }

    public KhachHang KhachHang { get; set; }

    public virtual VaiTro? VaiTro { get; set; }
}
