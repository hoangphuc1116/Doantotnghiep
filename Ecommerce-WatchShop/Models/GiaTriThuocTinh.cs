using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class GiaTriThuocTinh
{
    [Key]
    public int MaGiaTri { get; set; }

    public int MaThuocTinh { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Required(ErrorMessage = "Giá trị là bắt buộc")]
    public string GiaTri { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    public string? Slug { get; set; }

    public int ThuTuHienThi { get; set; } = 0;

    [ForeignKey("MaThuocTinh")]
    public virtual ThuocTinhSanPham? ThuocTinh { get; set; }

    public virtual ICollection<SanPhamThuocTinh> SanPhamThuocTinhs { get; set; } = new List<SanPhamThuocTinh>();
}
