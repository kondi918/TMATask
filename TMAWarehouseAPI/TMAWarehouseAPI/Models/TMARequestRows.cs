using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMAWarehouseAPI.Models
{
    public class TMARequestRows
    {
        [Key]
        public int RequestRowID { get; set; }

        [Required]
        public int RequestID { get; set; }

        [Required]
        public int ItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceWithoutVAT { get; set; }

        [ForeignKey("RequestID")]
        public TMARequest TMARequest { get; set; }

        [ForeignKey("ItemID")]
        public Item Item { get; set; }

    }
}
