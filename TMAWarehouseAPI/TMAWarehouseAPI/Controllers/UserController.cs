using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Exceptions;
using TMAWarehouseAPI.Models;
using TMAWarehouseAPI.Models.DTO;
using TMAWarehouseAPI.Services;

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
 
        [HttpPost("PostUser", Name = "PostUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUser([FromBody] UserDTO userDTO)
        {
            UsersService loginService = new UsersService(_databaseContext);
            if (userDTO.Username == "inituserstmawarehouse" && userDTO.Password == "initusershashedpasswords")
            {
                if (await loginService.AddUsers())
                {
                    return CreatedAtAction(nameof(GetUser), userDTO);
                }
                return StatusCode(500, $"Error occured durning adding users to database");
            }
            else
            {
                try
                {
                    User user = await loginService.GetUser(userDTO.Username, userDTO.Password);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    if(ex is UserNotFoundException)
                    {
                        return NotFound(ex.Message);
                    }
                    return StatusCode(500, $"Error occured durning getting users from database");
                }
            }
        }
        [HttpGet("GetUsers", Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TMARequestRowsResponseDTO>>> GetAllUsers()
        {
            try
            {
                UsersService usersService = new UsersService(_databaseContext);
                var response = await usersService.GetAllUsers();
                if (response.Count > 0)
                {
                    return Ok(response);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
