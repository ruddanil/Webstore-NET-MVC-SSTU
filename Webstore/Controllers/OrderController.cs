using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webstore.Service.Interfaces;

namespace Webstore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult ReadOrders()
        {
            var response = _orderService.ReadOrders();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var response = await _orderService.DeleteOrder(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("ReadOrders");
            }
            return View("Error", $"{response.Description}");
        }
    }
}
