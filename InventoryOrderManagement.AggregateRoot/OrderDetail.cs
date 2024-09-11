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

    public class OrderDetailExportService
    {
        public byte[] ExportOrderDetailsToCsv(List<AddDetailOrderViewModelDTO> orderDetails)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Product Name,Quantity,Unit Price");

            foreach (var orderDetail in orderDetails)
            {
                builder.AppendLine($"{orderDetail.ProductName},{orderDetail.Quantity},{orderDetail.UnitPrice}");
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public byte[] ExportOrderDetailsToPdf(List<AddDetailOrderViewModelDTO> orderDetails)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;
                table.AddCell(new PdfPCell(new Phrase("Product Name")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Quantity")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Unit Price")) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.LIGHT_GRAY });

                foreach (var orderDetail in orderDetails)
                {
                    table.AddCell(orderDetail.ProductName ?? string.Empty);
                    table.AddCell(orderDetail.Quantity.ToString());
                    table.AddCell(orderDetail.UnitPrice.ToString("F2"));
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                return stream.ToArray();
            }
        }
    }
}