using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class YeuThich
{
    [Key]
    public int MaYeuThich { get; set; }

    [Required]
    public int MaSanPham { get; set; }

    public int? MaKhachHang { get; set; }

    [ForeignKey(nameof(MaKhachHang))]
    public virtual KhachHang? KhachHang { get; set; }

    [ForeignKey(nameof(MaSanPham))]
    public virtual SanPham SanPham { get; set; }
}