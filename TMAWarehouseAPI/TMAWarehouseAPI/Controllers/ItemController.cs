using Microsoft.AspNetCore.Mvc;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models;
using TMAWarehouseAPI.Services;
using System.Diagnostics;
using TMAWarehouseAPI.Models.DTO;

namespace TMAWarehouseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public ItemController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("GetItems", Name = "GetItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ItemDTOWithID>>> GetAllItems()
        {
            ItemsService itemsService = new ItemsService(_databaseContext);
            try
            {
                var items = await itemsService.GetAllItems();
                if(items == null || items.Count == 0)
                {
                    return NotFound("Cannot find any item in database");
                }
                return Ok(items);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostItem", Name = "PostItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> AddItem([FromBody] ItemDTO itemDTO)
        {
            ItemsService itemsService = new ItemsService(_databaseContext);
            try
            {
                var result = await itemsService.AddItem(itemDTO);
                if(result)
                {
                    return Ok(true);
                }
                return StatusCode(500,false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("UpdateItem", Name = "UpdateItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateItem([FromBody] ItemDTOWithID itemDTO)
        {
            ItemsService itemsService = new ItemsService(_databaseContext);
            try
            {
                var result = await itemsService.UpdateItem(itemDTO);
                if (result)
                {
                    return Ok(true);
                }
                return StatusCode(500, false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("DeleteItem", Name = "DeleteItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteItem([FromBody] int id)
        {
            ItemsService itemsService = new ItemsService(_databaseContext);
            try
            {
                var result = await itemsService.DeleteItem(id);
                if (result)
                {
                    return Ok(true);
                }
                return StatusCode(500, "Couldnt find any item with id:" + id + "in database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
