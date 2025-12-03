using Ecommerce_WatchShop.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class ChiTietHoaDon
{
    [Key]
    public int MaChiTietHoaDon { get; set; }

    public int MaHoaDon { get; set; }

    public int MaSanPham { get; set; }

    [Precision(18, 0)]
    public decimal Gia { get; set; }

    public int SoLuong { get; set; }

    [Precision(18, 0)]
    public decimal TongTien { get; set; }

    [ForeignKey("MaHoaDon")]
    public virtual HoaDon HoaDon { get; set; } = null!;

    [ForeignKey("MaSanPham")]
    public virtual SanPham SanPham { get; set; } = null!;
}