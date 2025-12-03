
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models
{
    public class Huyen
    {
        [Key]
        public int MaHuyen { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHuyen { get; set; } = null!;

        public int MaTinh { get; set; }
        public Tinh Tinh { get; set; } = null!;

        public ICollection<Xa> Xas { get; set; } = new List<Xa>();
    }
}
