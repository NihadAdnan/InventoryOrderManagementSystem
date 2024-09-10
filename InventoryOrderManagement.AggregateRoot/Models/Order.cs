using System.ComponentModel.DataAnnotations;

namespace InventoryOrderManagement.AggregateRoot.Models
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        [Required]
        [StringLength(100)]
        public string? CustomerName { get; set; }

        [StringLength(100)]
        public string? CustomerEmail { get; set; }

        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        [StringLength(255)]
        public string? ShippingAddress { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Required]
        [Range(0, 999999999999.99)]
        public decimal TotalAmount { get; set; }
    }
}