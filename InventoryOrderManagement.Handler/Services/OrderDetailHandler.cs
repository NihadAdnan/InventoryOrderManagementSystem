using InventoryOrderManagement.AggregateRoot.BusinessLogic;
using InventoryOrderManagement.AggregateRoot.Models;
using InventoryOrderManagement.Handler.Interfaces;
using InventoryOrderManagement.Repository.GenericRepositories;
using InventoryOrderManagement.AggregateRoot.Mappers;
using InventoryOrderManagement.DTO.DTOs;
namespace InventoryOrderManagement.Handler.Services
{
    public class OrderDetailHandler : IOrderDetailHandler
    {
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly ExportService _exportService;

        public OrderDetailHandler(IGenericRepository<OrderDetail> orderDetailRepository, ExportService exportService)
        {
            _orderDetailRepository = orderDetailRepository;
            _exportService = exportService;
        }

        public async Task<List<AddDetailOrderViewModelDTO>> GetOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            return orderDetails.Select(orderDetail => orderDetail.ToOrderDetailViewModel()).ToList();
        }

        public async Task<AddDetailOrderViewModelDTO?> GetOrderDetailByIdAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
            return orderDetail != null ? orderDetail.ToOrderDetailViewModel() : null;
        }

        public async Task AddOrderDetailAsync(AddDetailOrderViewModelDTO orderDetailDto)
        {
            var orderDetail = orderDetailDto.ToOrderDetailModel();
            await _orderDetailRepository.AddAsync(orderDetail);
        }

        public async Task UpdateOrderDetailAsync(AddDetailOrderViewModelDTO orderDetailDto)
        {
            var existingOrderDetail = await _orderDetailRepository.GetByIdAsync(orderDetailDto.OrderDetailID);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.ProductName = orderDetailDto.ProductName;
                existingOrderDetail.Quantity = orderDetailDto.Quantity;
                existingOrderDetail.UnitPrice = orderDetailDto.UnitPrice;
                await _orderDetailRepository.UpdateAsync(existingOrderDetail);
            }
        }

        public async Task DeleteOrderDetailAsync(Guid id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
            if (orderDetail != null)
            {
                await _orderDetailRepository.DeleteAsync(orderDetail);
            }
        }

        public async Task<byte[]> ExportOrderDetailsToCsvAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            var orderDetailDtos = orderDetails.Select(orderDetail => orderDetail.ToOrderDetailViewModel()).ToList();
            return _exportService.ExportOrderDetailsToCsv(orderDetailDtos);
        }

        public async Task<byte[]> ExportOrderDetailsToPdfAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();
            var orderDetailDtos = orderDetails.Select(orderDetail => orderDetail.ToOrderDetailViewModel()).ToList();
            return _exportService.ExportOrderDetailsToPdf(orderDetailDtos);
        }
    }
}
