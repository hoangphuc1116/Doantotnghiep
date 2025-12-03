using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class VaiTro
{
    [Key]
    public int MaVaiTro { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string? Loai { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
