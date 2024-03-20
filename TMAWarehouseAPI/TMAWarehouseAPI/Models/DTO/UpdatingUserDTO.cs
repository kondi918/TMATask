namespace TMAWarehouseAPI.Models.DTO
{
    public class UpdatingUserDTO
    {
        public int Id { get; set; } 
        public string Username { get; set; }

        public string Password { get; set; }    
        public string Role { get; set; }
    }
}
