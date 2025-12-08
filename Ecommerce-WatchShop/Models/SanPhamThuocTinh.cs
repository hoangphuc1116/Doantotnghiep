using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class SanPhamThuocTinh
{
    [Key]
    public int Id { get; set; }

    public int MaSanPham { get; set; }

    public int MaGiaTri { get; set; }

    [ForeignKey("MaSanPham")]
    public virtual SanPham? SanPham { get; set; }

    [ForeignKey("MaGiaTri")]
    public virtual GiaTriThuocTinh? GiaTriThuocTinh { get; set; }
}
