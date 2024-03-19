using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMAWarehouseAPI.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemGroup { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceWithoutVAT { get; set; }

        [Required]
        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string StorageLocation { get; set; }

        [StringLength(255)]
        public string ContactPerson { get; set; }

        public byte[] Photo { get; set; }

    }
}
