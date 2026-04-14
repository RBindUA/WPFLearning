using ServiceOrderAPI.Models;

namespace ServiceOrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderHeader>> GetOrdersByCustomerIdAsync(int customerId);

        Task<bool> CreateOrderAsync(OrderHeader order);
    }
}
