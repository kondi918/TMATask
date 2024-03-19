namespace TMAWarehouseAPI.Models
{
    public class TMARowRequestModel
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public TMARequestModel TMARequest { get; set; }
        public Item Item { get; set; }

    }
}
