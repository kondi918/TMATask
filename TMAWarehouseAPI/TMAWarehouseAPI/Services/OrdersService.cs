using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Models;
using TMAWarehouseAPI.Models.DTO;

namespace TMAWarehouseAPI.Services
{
    public class OrdersService
    {
        private readonly DatabaseContext _dbContext;
        public OrdersService(DatabaseContext _context)
        {
            _dbContext = _context;
        }
        private TMARequestModel GetTMARequest(string employeeName, string comment, string status )
        {
            TMARequestModel model = new TMARequestModel
            {
                EmployeeName = employeeName,
                Comment = comment,
                Status = status
            };
            return model;
        }
        public async Task<List<int>> AddOrderRequest(List<TMARowRequestDTO> requestBody)
        {
            List<int> testList = new List<int>();
            try
            {
                foreach (var element in requestBody)
                {
                    testList.Add(element.ItemID);
                }
                return testList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
