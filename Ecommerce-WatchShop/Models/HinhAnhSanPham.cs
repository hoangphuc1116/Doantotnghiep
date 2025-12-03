using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class HinhAnhSanPham
{
    [Key]
    public int MaHinhAnhSanPham { get; set; }

    public int? MaSanPham { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? HinhAnh { get; set; }

    public virtual SanPham? SanPham { get; set; }
}
