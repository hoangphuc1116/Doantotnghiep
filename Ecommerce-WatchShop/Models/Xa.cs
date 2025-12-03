
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models
{
    public class Xa
    {
        [Key]
        public int MaXa { get; set; }

        [Required]
        [StringLength(100)]
        public string TenXa { get; set; } = null!;

        public int MaHuyen { get; set; }
        public Huyen Huyen { get; set; } = null!;
    }
}
