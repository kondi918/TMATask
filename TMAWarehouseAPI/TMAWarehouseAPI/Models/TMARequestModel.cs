namespace TMAWarehouseAPI.Models
{
    public class TMARequestModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public List<TMARowRequestModel> RequestRows { get; set; }

    }
}
