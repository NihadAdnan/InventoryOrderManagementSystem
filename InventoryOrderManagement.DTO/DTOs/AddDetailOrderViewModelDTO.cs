
namespace InventoryOrderManagement.DTO.DTOs
{
    public class AddDetailOrderViewModelDTO
    {
        public Guid OrderDetailID { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}