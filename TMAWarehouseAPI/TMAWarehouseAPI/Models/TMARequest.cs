using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace TMAWarehouseAPI.Models
{
    public class TMARequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        [StringLength(255)]
        public string EmployeeName { get; set; }

        public string Comment { get; set; }

        [StringLength(255)]
        public string Status { get; set; } = "New";
    }
}
