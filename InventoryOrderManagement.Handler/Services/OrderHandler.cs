using InventoryOrderManagement.AggregateRoot.BusinessLogic;
using InventoryOrderManagement.AggregateRoot.Models;
using InventoryOrderManagement.DTO.DTOs;
using InventoryOrderManagement.Handler.Interfaces;
using InventoryOrderManagement.Repository.GenericRepositories;
using InventoryOrderManagement.AggregateRoot.Mappers; 

namespace InventoryOrderManagement.Handler.Services
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly ExportService _exportService;

        public OrderHandler(IGenericRepository<Order> orderRepository, ExportService exportService)
        {
            _orderRepository = orderRepository;
            _exportService = exportService;
        }

        public async Task<List<AddOrderViewModelDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(order => order.ToOrderViewModel()).ToList();
        }

        public async Task<AddOrderViewModelDTO?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? order.ToOrderViewModel() : null;
        }

        public async Task AddOrderAsync(AddOrderViewModelDTO orderDto)
        {
            var order = orderDto.ToOrderModel();
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(AddOrderViewModelDTO orderDto)
        {
            var order = orderDto.ToOrderModel();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
            }
        }

        public async Task<byte[]> ExportOrdersToCsvAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = orders.Select(order => order.ToOrderViewModel()).ToList();
            return _exportService.ExportOrdersToCsv(orderDtos);
        }

        public async Task<byte[]> ExportOrdersToPdfAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = orders.Select(order => order.ToOrderViewModel()).ToList();
            return _exportService.ExportOrdersToPdf(orderDtos);
        }
    }
}
