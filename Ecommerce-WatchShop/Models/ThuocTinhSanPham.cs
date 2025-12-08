using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class ThuocTinhSanPham
{
    [Key]
    public int MaThuocTinh { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Required(ErrorMessage = "Tên thuộc tính là bắt buộc")]
    public string TenThuocTinh { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    public string? Slug { get; set; }

    [Column(TypeName = "nvarchar(255)")]
    public string? MoTa { get; set; }

    public int ThuTuHienThi { get; set; } = 0;

    public bool HienThi { get; set; } = true;

    public virtual ICollection<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; } = new List<GiaTriThuocTinh>();
}
