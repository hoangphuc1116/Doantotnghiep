using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class DanhMuc
{
    [Key]
    public int MaDanhMuc { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string? TenDanhMuc { get; set; }

    public int? MaDanhMucCha { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string? Slug { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string? HinhAnh { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
