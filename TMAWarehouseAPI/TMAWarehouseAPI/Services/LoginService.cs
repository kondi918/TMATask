using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models;

namespace TMAWarehouseAPI.Services
{
    public class LoginService
    {
        private readonly DatabaseContext _dbContext;

        public LoginService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
