using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class HinhAnhBaiViet
{
    [Key]
    public int MaHinhAnhBaiViet { get; set; }

    public int? MaBaiViet { get; set; }
    [Column(TypeName = "nvarchar(255)")]
    public string? NoiDung { get; set; }
    public string? HinhAnh { get; set; }

    public virtual BaiViet? BaiViet { get; set; }
}
