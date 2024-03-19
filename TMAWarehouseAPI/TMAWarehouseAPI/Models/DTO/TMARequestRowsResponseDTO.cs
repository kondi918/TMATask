namespace TMAWarehouseAPI.Models.DTO
{
    public class TMARequestRowsResponseDTO
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal PriceWithoutVAT { get; set; }
    }
}
