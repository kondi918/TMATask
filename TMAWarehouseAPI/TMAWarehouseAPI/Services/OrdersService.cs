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
        private TMARequest GetTMARequest(string employeeName, string comment, string status )
        {
            TMARequest model = new TMARequest
            {
                EmployeeName = employeeName,
                Comment = comment,
                Status = status
            };
            return model;
        }
        public async Task<bool> AddOrderRequest(List<TMARowRequestDTO> requestBody)
        {
            try
            {
                var tmaRequest = new TMARequest
                {
                    EmployeeName = requestBody.FirstOrDefault()?.EmployeeName,
                    Comment = "",
                    Status = "New",
                };

                _dbContext.TMARequests.Add(tmaRequest);

                await _dbContext.SaveChangesAsync();


                foreach (var element in requestBody)
                {
                    var tmaRowRequest = new TMARequestRows
                    {
                        RequestID = tmaRequest.RequestID,
                        ItemID = element.ItemID,
                        Quantity = element.Quantity,
                        UnitOfMeasurement = element.UnitOfMeasurement,
                        PriceWithoutVAT = element.PriceWithoutVAT,
                    };

                    _dbContext.TMARequestRows.Add(tmaRowRequest);
                }

                await _dbContext.SaveChangesAsync();

                return true;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
