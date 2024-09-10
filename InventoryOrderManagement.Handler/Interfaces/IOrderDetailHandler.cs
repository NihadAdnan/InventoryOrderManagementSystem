using InventoryOrderManagement.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using InventoryManageMate.DTO.DTOs;


namespace InventoryOrderManagement.Handler.Interfaces
{
    public interface IOrderDetailHandler
    {
        Task<List<AddDetailOrderViewModelDTO>> GetOrderDetailsAsync();
        Task<AddDetailOrderViewModelDTO?> GetOrderDetailByIdAsync(Guid id);
        Task AddOrderDetailAsync(AddDetailOrderViewModelDTO orderDetailDto);
        Task UpdateOrderDetailAsync(AddDetailOrderViewModelDTO orderDetailDto);
        Task DeleteOrderDetailAsync(Guid id);
        Task<byte[]> ExportOrderDetailsToCsvAsync();
        Task<byte[]> ExportOrderDetailsToPdfAsync();
    }
}