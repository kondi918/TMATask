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
                if (await loginService.AddDefaultUsers())
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
        [HttpPost("AddUser", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> AddUser([FromBody] AddingUserDTO userDTO)
        {
            try
            {
                UsersService usersService = new UsersService(_databaseContext);
                var result = await usersService.AddUser(userDTO);
                if(result)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("UpdateUser", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdatingUserDTO userDTO)
        {
            try
            {
                UsersService usersService = new UsersService(_databaseContext);
                var result = await usersService.UpdateUser(userDTO);
                if (result)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("RemoveUser", Name = "RemoveUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> RemoveUser([FromBody] int id)
        {
            try
            {
                UsersService usersService = new UsersService(_databaseContext);
                var result = await usersService.RemoveUser(id);
                if (result)
                {
                    return Ok(result);
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
