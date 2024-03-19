using Microsoft.AspNetCore.Mvc;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models.DTO;
using TMAWarehouseAPI.Services;

namespace TMAWarehouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TMARequestController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public TMARequestController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("PostOrderRequest", Name = "PostOrderRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> PostRequest([FromBody] List<TMARowRequestDTO> requestRows)
        {
            try
            {
                OrdersService ordersService = new OrdersService(_dbContext);
                if (requestRows.Count > 0)
                {
                    var  result = await ordersService.AddOrderRequest(requestRows);
                    if(result)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
