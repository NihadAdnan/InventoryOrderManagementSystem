using InventoryOrderManagement.DTO.DTOs;
using InventoryOrderManagement.Handler.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderManagement.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderHandler _orderHandler;

        public OrdersController(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await _orderHandler.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderViewModelDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderHandler.AddOrderAsync(orderDto);
                return RedirectToAction("List");
            }
            return View(orderDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var orderDto = await _orderHandler.GetOrderByIdAsync(id);
            if (orderDto == null)
            {
                return NotFound();
            }
            return View(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddOrderViewModelDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderHandler.UpdateOrderAsync(orderDto);
                return RedirectToAction("List");
            }
            return View(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("List");
            }
            await _orderHandler.DeleteOrderAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToCsv()
        {
            var fileContent = await _orderHandler.ExportOrdersToCsvAsync();
            return File(fileContent, "text/csv", "OrderHistory.csv");
        }

        [HttpGet]
        public async Task<IActionResult> ExportToPdf()
        {
            var fileContent = await _orderHandler.ExportOrdersToPdfAsync();
            return File(fileContent, "application/pdf", "OrderHistory.pdf");
        }
    }
}
