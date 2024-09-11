using InventoryOrderManagement.DTO.DTOs;

namespace InventoryOrderManagement.Handler.Interfaces
{
    public interface IOrderHandler
    {
        Task<List<AddOrderViewModelDTO>> GetAllOrdersAsync();
        Task<AddOrderViewModelDTO?> GetOrderByIdAsync(Guid id);
        Task AddOrderAsync(AddOrderViewModelDTO orderDto);
        Task UpdateOrderAsync(AddOrderViewModelDTO orderDto);
        Task DeleteOrderAsync(Guid id);
        Task<byte[]> ExportOrdersToCsvAsync();
        Task<byte[]> ExportOrdersToPdfAsync();
    }
}