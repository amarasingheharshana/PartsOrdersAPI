using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Interfaces
{
    public interface IOrdersService
    {
        Order PlaceOrder(List<OrderRequest> lineItems);
        IEnumerable<Order> GetAllOrders();
    }
}
