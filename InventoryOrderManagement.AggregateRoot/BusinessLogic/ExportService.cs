using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryOrderManagement.DTO.DTOs;

namespace InventoryOrderManagement.AggregateRoot.BusinessLogic
{
    public class ExportService
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




