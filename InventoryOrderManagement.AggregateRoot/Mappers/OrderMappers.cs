using InventoryOrderManagement.AggregateRoot.Models;
using InventoryOrderManagement.DTO.DTOs;
using System;

namespace InventoryOrderManagement.AggregateRoot.Mappers
{
    public static class OrderMappers
    {
        public static AddOrderViewModelDTO ToOrderViewModel(this Order orderModel)
        {
            return new AddOrderViewModelDTO
            {
                OrderID = orderModel.OrderID,
                CustomerName = orderModel.CustomerName,
                CustomerEmail = orderModel.CustomerEmail,
                TotalAmount = orderModel.TotalAmount,
                PaymentMethod = orderModel.PaymentMethod,
                ShippingAddress = orderModel.ShippingAddress
            };
        }

        public static Order ToOrderModel(this AddOrderViewModelDTO orderViewModel)
        {
            return new Order
            {
                OrderID = orderViewModel.OrderID,
                CustomerName = orderViewModel.CustomerName,
                CustomerEmail = orderViewModel.CustomerEmail,
                TotalAmount = orderViewModel.TotalAmount,
                PaymentMethod = orderViewModel.PaymentMethod,
                ShippingAddress = orderViewModel.ShippingAddress,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending" 
            };
        }
    }
}