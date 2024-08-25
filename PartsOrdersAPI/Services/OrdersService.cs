using PartsOrdersAPI.Interfaces;
using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Services
{
    /// <summary>
    /// Service to manage orders, including placing new orders and retrieving all placed orders.
    /// Orders and parts are stored in memory for simplicity.
    /// </summary>
    public class OrdersService : IOrdersService
    {
        private readonly IPartsService _partsService;
        private readonly List<Order> _orders = new List<Order>();
        private int _nextOrderNumber = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersService"/> class.
        /// </summary>
        /// <param name="partsService">The service to manage parts, used to validate and update parts when orders are placed.</param>
        public OrdersService(IPartsService partsService)
        {
            _partsService = partsService;
        }

        /// <summary>
        /// Places a new order with the specified line items.
        /// Each line item reduces the corresponding part's quantity in inventory.
        /// </summary>
        /// <param name="lineItems">The list of line items that make up the order.</param>
        /// <returns>The placed order, including the order number and total costs.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a part in the order is not available or has insufficient quantity.</exception>
        public Order PlaceOrder(List<OrderRequest> lineItems)
        {
            var order = new Order { OrderNumber = _nextOrderNumber++ };
            foreach (var lineItem in lineItems)
            {
                var part = _partsService.GetPartById(lineItem.PartId);
                if (part == null || part.Quantity < lineItem.Quantity)
                {
                    throw new InvalidOperationException("Part not available or insufficient quantity.");
                }

                part.Quantity -= lineItem.Quantity;
                order.LineItems.Add(new OrderLineItem
                {
                    PartId = part.Id,
                    PartDescription = part.Description,
                    Price = part.Price,
                    Quantity = lineItem.Quantity
                });
            }
            _orders.Add(order);
            return order;
        }

        /// <summary>
        /// Retrieves all orders that have been placed.
        /// </summary>
        /// <returns>A collection of all placed orders.</returns>
        public IEnumerable<Order> GetAllOrders() => _orders;
    }
}
