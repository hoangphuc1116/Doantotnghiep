using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models
{
    public partial class Slider
    {
        [Key]
        public int Ma { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? TieuDe { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public string? Link { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool TrangThai { get; set; } = true;
        public bool HienThiTrangChu { get; set; } = false;
        public bool HienThiTrangSanPham { get; set; } = false;


    }
}
