using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class LienHe
{
    [Key]
    public int MaLienHe { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    public string HoTen { get; set; } = null!;
    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; } = null!;
    [Column(TypeName = "varchar(200)")]
    public string TieuDe { get; set; } = null!;
    [Column(TypeName = "nvarchar(500)")]
    public string GhiChu { get; set; } = null!;

    public int TrangThai { get; set; } = 0;
}
