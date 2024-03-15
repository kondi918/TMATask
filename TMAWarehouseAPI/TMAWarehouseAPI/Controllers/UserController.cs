using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models;

namespace TMAWarehouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public UserController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet(Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _databaseContext.Users.ToListAsync();
        }
    }
}
