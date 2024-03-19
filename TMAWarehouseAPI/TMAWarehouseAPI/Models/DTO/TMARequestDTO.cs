using System.ComponentModel.DataAnnotations;

namespace TMAWarehouseAPI.Models.DTO
{
    public class TMARequestDTO
    {
        public int RequestID { get; set; }
        public string EmployeeName { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
