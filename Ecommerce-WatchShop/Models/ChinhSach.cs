using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models;
public partial class ChinhSach
{
    [Key]
    public int Ma { get; set; }
    public string? TieuDe { get; set; }
    public string? NoiDung { get; set; }
}