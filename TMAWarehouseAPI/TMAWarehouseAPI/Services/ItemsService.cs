using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models;
using TMAWarehouseAPI.Models.DTO;

namespace TMAWarehouseAPI.Services
{
    public class ItemsService
    {
        private readonly DatabaseContext _dbContext;
        public ItemsService(DatabaseContext _context)
        {
            _dbContext = _context;
        }
        public async Task<List<ItemResponse>> GetAllItems()
        {
            try
            {
                var dbQueries = await _dbContext.Items.ToListAsync();
                List<ItemResponse> items = new List<ItemResponse>();
                foreach(var query in dbQueries)
                {
                    items.Add(MapToItemResponse(query));
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with executing query in database");
            }
        }
        private byte[] ConvertFromBase64(string base64String)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                return bytes;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid base64 string.");
            }
        }
        private string ConvertToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        private Item MapToItem(ItemDTO itemDTO)
        {
            byte[] mPhoto = null;
            if(itemDTO.Photo != null)
            {
                mPhoto = ConvertFromBase64(itemDTO.Photo);
            }
            return new Item
            {
                ItemGroup = itemDTO.ItemGroup,
                UnitOfMeasurement = itemDTO.UnitOfMeasurement,
                Quantity = itemDTO.Quantity,
                PriceWithoutVAT = itemDTO.PriceWithoutVAT,
                Status = itemDTO.Status,
                StorageLocation = itemDTO.StorageLocation,
                ContactPerson = itemDTO.ContactPerson,
                Photo = mPhoto
            };
        }
        private ItemResponse MapToItemResponse(Item item)
        {
            string mPhoto = String.Empty;
            if (item.Photo != null)
            {
                mPhoto = ConvertToBase64(item.Photo);
            }
            return new ItemResponse
            {
                ItemID = item.ItemID,
                ItemGroup = item.ItemGroup,
                UnitOfMeasurement = item.UnitOfMeasurement,
                Quantity = item.Quantity,
                PriceWithoutVAT = item.PriceWithoutVAT,
                Status = item.Status,
                StorageLocation = item.StorageLocation,
                ContactPerson = item.ContactPerson,
                Photo = mPhoto
            };
        }
        public async Task<bool> AddItem(ItemDTO newItem)
        {
            try
            {
                _dbContext.Items.Add(MapToItem(newItem));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the item:");
            }
        }
        // Tu kontynuowac
        public async Task<bool> UpdateItem(ItemDTO newItem)
        {
            try
            {
                _dbContext.Items.Add(MapToItem(newItem));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the item:");
            }
        }

    }
}
