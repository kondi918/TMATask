using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Exceptions;
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
        public async Task<List<ItemDTOWithID>> GetAllItems()
        {
            try
            {
                var dbQueries = await _dbContext.Items.ToListAsync();
                List<ItemDTOWithID> items = new List<ItemDTOWithID>();
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
        private ItemDTOWithID MapToItemResponse(Item item)
        {
            string mPhoto = String.Empty;
            if (item.Photo != null)
            {
                mPhoto = ConvertToBase64(item.Photo);
            }
            return new ItemDTOWithID
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
                var existingItem = await _dbContext.Items.FirstOrDefaultAsync(item => item.ItemGroup ==  newItem.ItemGroup && item.UnitOfMeasurement == newItem.UnitOfMeasurement);
                if(existingItem != null)
                {
                    throw new ExistingItemException("That item already exists in database! Consider update or remove");
                }
                _dbContext.Items.Add(MapToItem(newItem));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if(ex is ExistingItemException)
                {
                    throw new Exception(ex.Message);
                }
                throw new Exception($"An error occurred while adding the item:");
            }
        }
        public async Task<bool> UpdateItem(ItemDTOWithID updatedItem)
        {
            try
            {
                var existingItem = await _dbContext.Items.FindAsync(updatedItem.ItemID);
                if (existingItem == null)
                {
                    return false;
                }

                existingItem.ItemGroup = updatedItem.ItemGroup;
                existingItem.UnitOfMeasurement = updatedItem.UnitOfMeasurement;
                existingItem.Quantity = updatedItem.Quantity;
                existingItem.PriceWithoutVAT = updatedItem.PriceWithoutVAT;
                existingItem.Status = updatedItem.Status;
                existingItem.StorageLocation = updatedItem.StorageLocation;
                existingItem.ContactPerson = updatedItem.ContactPerson;
                existingItem.Photo = !string.IsNullOrEmpty(updatedItem.Photo) ? ConvertFromBase64(updatedItem.Photo) : null;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the item: {ex.Message}");
            }
        }
        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var itemToDelete = await _dbContext.Items.FindAsync(id);
                if (itemToDelete == null)
                {
                    return false;
                }
                _dbContext.Items.Remove(itemToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the item: {ex.Message}");
            }
        }

    }
}
