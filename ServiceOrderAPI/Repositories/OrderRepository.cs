using Microsoft.EntityFrameworkCore;
using ServiceOrderAPI.Data;
using ServiceOrderAPI.Models;

namespace ServiceOrderAPI.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.OrderHeaders
                    .Include(o => o.OrderLines)
                    .Where(o => o.CustomerID == customerId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();
        }
        public async Task<bool> CreateOrderAsync(OrderHeader order)
        {
            try
            {
                _context.OrderHeaders.Add(order);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
