using InventoryOrderManagement.DTO.DTOs;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryOrderManagement.AggregateRoot
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
    public class OrderExportService
    {
        public byte[] ExportOrdersToCsv(List<AddOrderViewModelDTO> orders)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Name,Email,Total Amount,Payment Method,Address");

            foreach (var order in orders)
            {
                builder.AppendLine($"{order.CustomerName},{order.CustomerEmail},{order.TotalAmount},{order.PaymentMethod},{order.ShippingAddress}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public byte[] ExportOrdersToPdf(List<AddOrderViewModelDTO> orders)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.AddCell(new PdfPCell(new Phrase("Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Email")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Total Amount")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Payment Method")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Address")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });

                foreach (var order in orders)
                {
                    table.AddCell(order.CustomerName ?? string.Empty);
                    table.AddCell(order.CustomerEmail ?? string.Empty);
                    table.AddCell(order.TotalAmount.ToString("F2"));
                    table.AddCell(order.PaymentMethod ?? string.Empty);
                    table.AddCell(order.ShippingAddress ?? string.Empty);
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return stream.ToArray();
            }
        }
    }


}