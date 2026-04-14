
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceOrderAPI.Data;
using ServiceOrderAPI.Models;
using ServiceOrderAPI.Repositories;

namespace ServiceOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("user/{customerId}")]
        public async Task<IActionResult> GetOrderHistory(int customerId)
        {
            var orders = await _repository.GetOrdersByCustomerIdAsync(customerId);

            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = $"No orders found for customer {customerId}." });
            }

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderHeader newOrder)
        {
            if (newOrder == null) return BadRequest("Order data is missing.");

            var success = await _repository.CreateOrderAsync(newOrder);

            if (!success)
            {
                return BadRequest(new { message = "Could not save the order to the database." });
            }

            return CreatedAtAction(nameof(GetOrderHistory), new { customerId = newOrder.CustomerID }, newOrder);
        }

    }
}
