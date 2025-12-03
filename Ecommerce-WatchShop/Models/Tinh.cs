
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_WatchShop.Models
{
    public class Tinh
    {
        [Key]
        public int MaTinh { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTinh { get; set; } = null!;

        public ICollection<Huyen> Huyens { get; set; } = new List<Huyen>();
    }
}
