using Microsoft.AspNetCore.Mvc;
using PartsOrdersAPI.Interfaces;
using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Controllers
{
    /// <summary>
    /// Controller to handle operations related to orders, such as placing orders and retrieving all orders.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="ordersService">The service used to manage orders.</param>
        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        /// <summary>
        /// Places a new order with the specified line items.
        /// </summary>
        /// <param name="lineItems">The list of line items for the order.</param>
        /// <returns>The placed order with order number and total costs.</returns>
        /// <response code="200">Returns the placed order with details.</response>
        /// <response code="400">If the line items are invalid or parts are insufficient.</response>
        [HttpPost("place_order")]
        public ActionResult<Order> PlaceOrder([FromBody] List<OrderRequest> lineItems)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid order data received.");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Placing new order.");
                var order = _ordersService.PlaceOrder(lineItems);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error placing order.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all orders that have been placed.
        /// </summary>
        /// <returns>A list of all placed orders.</returns>
        /// <response code="200">Returns the list of all orders.</response>
        [HttpGet("get_orders")]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            _logger.LogInformation("Fetching all orders.");
            var orders = _ordersService.GetAllOrders();
            return Ok(orders);
        }
    }
}
