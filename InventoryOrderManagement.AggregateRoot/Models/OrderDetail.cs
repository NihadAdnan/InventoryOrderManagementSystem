using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryOrderManagement.AggregateRoot.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailID { get; set; }

        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 999999999999.99)]
        public decimal UnitPrice { get; set; }

        [Range(0, 999999999999.99)]
        public decimal Discount { get; set; } = 0;
    }
}