namespace TMAWarehouseAPI.Models.DTO
{
    public class ItemResponse
    {
        public int ItemID { get; set; }
        public string ItemGroup { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string Status { get; set; }
        public string StorageLocation { get; set; }
        public string ContactPerson { get; set; }
        public string Photo { get; set; }
    }
}
