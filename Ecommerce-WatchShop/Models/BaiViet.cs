using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class BaiViet
{
    [Key]
    public int MaBaiViet { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string? HinhAnh { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? TieuDe { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? NoiDung { get; set; }

    public virtual ICollection<HinhAnhBaiViet> HinhAnhBaiViets { get; set; } = new List<HinhAnhBaiViet>();
}
