using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models;

public partial class DanhGiaSanPham
{
    [Key]
    public int MaDanhGia { get; set; }

    public int MaSanPham { get; set; }

    public int? MaKhachHang { get; set; }

    public int? DiemDanhGia { get; set; }

    public virtual KhachHang? KhachHang { get; set; }

    public virtual SanPham SanPham { get; set; } = null!;
}
