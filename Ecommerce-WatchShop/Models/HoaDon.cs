using Ecommerce_WatchShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models;

public partial class HoaDon
{
    [Key]
    public int MaHoaDon { get; set; }

    public int MaKhachHang { get; set; }

    public DateTime NgayDatHang { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string? HoTen { get; set; }

    [Required]
    [Column(TypeName = "varchar(15)")]
    public string? SoDienThoai { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string? Email { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(500)")]
    public string? DiaChi { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Tinh { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Huyen { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? Xa { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string? PhuongThucThanhToan { get; set; }
    [Column(TypeName = "decimal(18,0)")]
    public decimal TongTien { get; set; }

    public int TrangThai { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? YeuCauHuy { get; set; }

    public DateTime? NgayYeuCauHuy { get; set; }

    public bool DaYeuCauHuy { get; set; } = false;

    public virtual KhachHang? KhachHang { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
}