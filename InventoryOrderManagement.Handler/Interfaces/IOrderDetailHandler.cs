using InventoryOrderManagement.DTO.DTOs;


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