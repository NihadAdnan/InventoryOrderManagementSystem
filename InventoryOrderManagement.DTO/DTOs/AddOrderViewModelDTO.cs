
namespace InventoryOrderManagement.DTO.DTOs
{
    public class AddOrderViewModelDTO
    {
        public Guid OrderID { get; set; }
        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public decimal TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ShippingAddress { get; set; }

    }
}