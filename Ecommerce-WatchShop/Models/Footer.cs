using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_WatchShop.Models
{
    public partial class Footer
    {
        [Key]
        public int Ma { get; set; }

        public string? Logo { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? MoTa { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? DiaChi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }


        public string? FacebookUrl { get; set; }

        public bool TrangThai { get; set; }

    }
}
