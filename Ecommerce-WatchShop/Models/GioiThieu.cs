using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models;

public partial class GioiThieu
{
    [Key]
    public int Ma { get; set; }
    public string? NoiDung { get; set; }
    public string? DiaChi { get; set; }
    public string? SoDienThoai { get; set; }
    public string? Email { get; set; }
}
