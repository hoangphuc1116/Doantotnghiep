using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ecommerce_WatchShop.Models;

public partial class SanPham
{
    [Key]
    public int MaSanPham { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [AllowNull]
    public string? HinhAnh { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
    public string TenSanPham { get; set; } = null!;

    [Required(ErrorMessage = "Danh mục là bắt buộc.")]
    public int MaDanhMuc { get; set; }

    [Required(ErrorMessage = "Thương hiệu là bắt buộc.")]
    public int MaThuongHieu { get; set; }

    [Required(ErrorMessage = "Giá là bắt buộc.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
    public double Gia { get; set; }
    public double? GiaKhuyenMai { get; set; }

    [Range(0, 100, ErrorMessage = "Phần trăm khuyến mãi phải từ 0 đến 100.")]
    public decimal? PhanTramKhuyenMai { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string? MoTaNgan { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? MoTa { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? ThongSoKyThuat { get; set; }

    [Required(ErrorMessage = "Số lượng là bắt buộc.")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
    public int SoLuong { get; set; }

    public int LuotXem { get; set; } = 0;

    public int? TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? DaXoa { get; set; }

    [Column(TypeName = "varchar(255)")]   
    public string? Slug { get; set; }

    [ForeignKey("MaThuongHieu")]
    public virtual ThuongHieu? ThuongHieu { get; set; }

    [ForeignKey("MaDanhMuc")]
    public virtual DanhMuc? DanhMuc { get; set; }

    public virtual ICollection<YeuThich> YeuThichs { get; set; } = new List<YeuThich>();
    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    public virtual ICollection<BinhLuanSanPham> BinhLuanSanPhams { get; set; } = new List<BinhLuanSanPham>();
    public virtual ICollection<HinhAnhSanPham> HinhAnhSanPhams { get; set; } = new List<HinhAnhSanPham>();
    public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; } = new List<DanhGiaSanPham>();
    public virtual ICollection<SanPhamThuocTinh> SanPhamThuocTinhs { get; set; } = new List<SanPhamThuocTinh>();
}