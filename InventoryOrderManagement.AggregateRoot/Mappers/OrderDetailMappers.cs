using InventoryOrderManagement.AggregateRoot.Models;
using InventoryOrderManagement.DTO.DTOs;
using System;

namespace InventoryOrderManagement.AggregateRoot.Mappers
{
    public static class OrderDetailMappers
    {
        public static AddDetailOrderViewModelDTO ToOrderDetailViewModel(this OrderDetail orderDetailModel)
        {
            return new AddDetailOrderViewModelDTO
            {
                OrderDetailID = orderDetailModel.OrderDetailID,
                ProductName = orderDetailModel.ProductName,
                Quantity = orderDetailModel.Quantity,
                UnitPrice = orderDetailModel.UnitPrice
            };
        }

        public static OrderDetail ToOrderDetailModel(this AddDetailOrderViewModelDTO orderDetailViewModel)
        {
            return new OrderDetail
            {
                OrderDetailID = orderDetailViewModel.OrderDetailID,
                ProductName = orderDetailViewModel.ProductName,
                Quantity = orderDetailViewModel.Quantity,
                UnitPrice = orderDetailViewModel.UnitPrice,
                Discount = 0
            };
        }
    }
}