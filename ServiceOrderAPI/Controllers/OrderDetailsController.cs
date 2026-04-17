
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

        [HttpGet("user/{customerId}", Name = "GetOrderHistory")]
        public async Task<IActionResult> GetOrderHistory(int customerId)
        {
            var orders = await _repository.GetOrdersByCustomerIdAsync(customerId);

            if (orders == null)
            {
                //return 200 ok with list even if it`s empty
                return Ok(new List<OrderHeader>());
            }

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderHeader newOrder)
        {
            if (newOrder == null) return BadRequest("Order data is missing.");

            //Fix for db 
            newOrder.DueDate = DateTime.Now.AddDays(7);
            newOrder.ShipDate = DateTime.Now.AddDays(2);
            newOrder.Status = 1;
            newOrder.ModifiedDate = DateTime.Now;
            newOrder.rowguid = Guid.NewGuid();
            newOrder.BillToAddressID = 1;
            newOrder.ShipToAddressID = 1;
            newOrder.ShipMethodID = 1;
            newOrder.RevisionNumber = 1;
            var success = await _repository.CreateOrderAsync(newOrder);

            if (!success)
            {
                return BadRequest(new { message = "Could not save the order to the database." });
            }

            return CreatedAtRoute(nameof(GetOrderHistory), new { customerId = newOrder.CustomerID }, newOrder);
        }

    }
}
