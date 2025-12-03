using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class BinhLuanSanPham
{
    [Key]
    public int MaBinhLuan { get; set; }

    public int MaSanPham { get; set; }

    public int? MaKhachHang { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? NoiDung { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual KhachHang? KhachHang { get; set; }

    public virtual SanPham SanPham { get; set; } = null!;
}
