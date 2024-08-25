using PartsOrdersAPI.Interfaces;
using PartsOrdersAPI.Models;
using PartsOrdersAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsOrdersAPI.Test
{
    public class OrdersServiceTests
    {
        private readonly IOrdersService _ordersService;
        private readonly IPartsService _partsService;

        public OrdersServiceTests()
        {
            _partsService = new PartsService();
            _ordersService = new OrdersService(_partsService);
        }

        /// <summary>
        /// Test case to verify that placing an order reduces the quantity of the part in inventory.
        /// </summary>
        [Fact]
        public void PlaceOrder_ShouldReducePartQuantity()
        {
            // Arrange
            var lineItems = new List<OrderRequest>
        {
            new OrderRequest { PartId = 1, Quantity = 3 } // Order 3 Wire parts
        };

            // Act
            _ordersService.PlaceOrder(lineItems);
            var part = _partsService.GetPartById(1);

            // Assert
            Assert.Equal(2, part.Quantity); // Initially 5, after order 2 should remain
        }

        /// <summary>
        /// Test case to verify that placing an order with an invalid part ID throws an exception.
        /// </summary>
        [Fact]
        public void PlaceOrder_InvalidPartId_ShouldThrowException()
        {
            // Arrange
            var lineItems = new List<OrderRequest>
        {
            new OrderRequest { PartId = 999, Quantity = 1 } // Invalid part ID
        };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _ordersService.PlaceOrder(lineItems));
            Assert.Equal("Part not available or insufficient quantity.", exception.Message);
        }

        /// <summary>
        /// Test case to verify that placing an order returns the correct total cost.
        /// </summary>
        [Fact]
        public void PlaceOrder_ShouldReturnCorrectTotalCost()
        {
            // Arrange
            var lineItems = new List<OrderRequest>
        {
            new OrderRequest { PartId = 1, Quantity = 2 }, // 2 Wire parts
            new OrderRequest { PartId = 2, Quantity = 1 }  // 1 Brake Fluid part
        };

            // Act
            var order = _ordersService.PlaceOrder(lineItems);

            // Assert
            var expectedTotalCost = 2 * 5.99M + 4.90M; // 2 * Wire price + 1 * Brake Fluid price
            Assert.Equal(expectedTotalCost, order.TotalCost);
        }

        /// <summary>
        /// Test case to verify that the list of all orders returns the correct number of orders.
        /// </summary>
        [Fact]
        public void GetAllOrders_ShouldReturnCorrectOrderCount()
        {
            // Arrange
            var lineItems = new List<OrderRequest>
        {
            new OrderRequest { PartId = 1, Quantity = 2 }
        };

            // Act
            _ordersService.PlaceOrder(lineItems);
            var orders = _ordersService.GetAllOrders();

            // Assert
            Assert.Single(orders); // Expecting only one order in the list
        }

        /// <summary>
        /// Test case to verify that the order details include correct line item totals.
        /// </summary>
        [Fact]
        public void PlaceOrder_ShouldReturnCorrectLineItemTotals()
        {
            // Arrange
            var lineItems = new List<OrderRequest>
        {
            new OrderRequest { PartId = 1, Quantity = 2 }, // 2 Wire parts
        };

            // Act
            var order = _ordersService.PlaceOrder(lineItems);

            // Assert
            var expectedTotal = 2 * 5.99M; // 2 * Wire price
            Assert.Equal(expectedTotal, order.LineItems.First().Total);
        }
    }
}
