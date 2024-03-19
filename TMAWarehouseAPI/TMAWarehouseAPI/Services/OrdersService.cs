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

        public async Task<bool> AddOrderRequest(List<OrderRequestDTO> requestBody)
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
        private TMARequestDTO MapToTMARequestDTO(TMARequest element)
        {
            TMARequestDTO responseElement = new TMARequestDTO
            {
                RequestID = element.RequestID,
                EmployeeName = element.EmployeeName,
                Comment = element.Comment,
                Status = element.Status,
            };
            return responseElement;
        }
        public async Task<List<TMARequestDTO>> GetAllRequests(){
            var databaseList = await _dbContext.TMARequests.ToListAsync();
            List<TMARequestDTO> responseList = new List<TMARequestDTO>();
            foreach(var element in databaseList)
            {
                responseList.Add(MapToTMARequestDTO(element));
            }
            return responseList;

        }
        private async Task<List<TMARequestRowsResponseDTO>> SetTMARowsResponse(List<TMARequestRows> requestRows)
        {
            List<TMARequestRowsResponseDTO> response = new List<TMARequestRowsResponseDTO>();
            foreach(var row in requestRows)
            {
                TMARequestRowsResponseDTO singleResponse = new TMARequestRowsResponseDTO();
                singleResponse.itemID = row.ItemID;
                singleResponse.UnitOfMeasurement = row.UnitOfMeasurement;
                singleResponse.PriceWithoutVAT = row.PriceWithoutVAT;
                singleResponse.Quantity = row.Quantity;
                singleResponse.itemName = await _dbContext.Items.Where(item => item.ItemID == row.ItemID).Select(item => item.ItemGroup).FirstOrDefaultAsync();
                response.Add(singleResponse);
            }
            return response;
        }
        public async Task<List<TMARequestRowsResponseDTO>> GetRequestRows(int requestID){
            try
            {
                var requestRows = await _dbContext.TMARequestRows.Where(item => item.RequestID == requestID).ToListAsync();
                var response = await SetTMARowsResponse(requestRows);
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while trying to get request rows");
            }
        }
        public async Task<bool> RejectRequest(RejectRequestDTO requestBody)
        {
            try
            {
                var requestFromDB = await _dbContext.TMARequests.Where(item => item.RequestID == requestBody.RequestID).FirstOrDefaultAsync();
                if (requestFromDB == null)
                {
                    return false;
                }
                requestFromDB.Status = requestBody.Status;
                requestFromDB.Comment = requestBody.Comment;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while trying to update request");
            }
        }

        public async Task<bool> ConfirmRequest(int requestID)
        {
            try
            {
                var requestRows = await _dbContext.TMARequestRows.Where(item => item.RequestID == requestID).ToListAsync();
                if(requestRows.Count <= 0)
                {
                    return false;
                }
                foreach(var requestRow in requestRows)
                {
                    var singleItem = await _dbContext.Items.Where(item => item.ItemID == requestRow.ItemID).FirstOrDefaultAsync();
                    singleItem.Quantity -= requestRow.Quantity;
                }
                var request = await _dbContext.TMARequests.Where(item => item.RequestID == requestID).FirstOrDefaultAsync();
                if(request == null)
                {
                    return false;
                }
                request.Status = "done";
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while trying to update request");
            }
        }
    }
}
