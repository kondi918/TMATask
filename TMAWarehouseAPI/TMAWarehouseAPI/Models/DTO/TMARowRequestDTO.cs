namespace TMAWarehouseAPI.Models.DTO
{
    public class TMARowRequestDTO
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string? Comment { get; set; }
        public string EmployeeName { get; set; }

    }
}
