namespace TMAWarehouseAPI.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemGroup { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Quantity { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public string Status { get; set; }
        public string StorageLocation { get; set; }
        public string ContactPerson { get; set; }
        public byte[] Photo { get; set; }
    }
}
