using InventoryOrderManagement.DTO.DTOs;
using InventoryOrderManagement.Handler.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventoryManageMate.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailHandler _orderDetailHandler;

        public OrderDetailsController(IOrderDetailHandler orderDetailHandler)
        {
            _orderDetailHandler = orderDetailHandler;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orderDetails = await _orderDetailHandler.GetOrderDetailsAsync();
            return View(orderDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDetailOrderViewModelDTO orderDetailDto)
        {
            await _orderDetailHandler.AddOrderDetailAsync(orderDetailDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var orderDetail = await _orderDetailHandler.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddDetailOrderViewModelDTO orderDetailDto)
        {
            await _orderDetailHandler.UpdateOrderDetailAsync(orderDetailDto);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderDetailHandler.DeleteOrderDetailAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv()
        {
            var fileContent = await _orderDetailHandler.ExportOrderDetailsToCsvAsync();
            return File(fileContent, "text/csv", "OrderDetails.csv");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var fileContent = await _orderDetailHandler.ExportOrderDetailsToPdfAsync();
            return File(fileContent, "application/pdf", "OrderDetails.pdf");
        }
    }
}
