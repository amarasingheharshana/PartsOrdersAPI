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
    public class PartsServiceTests
    {
        private readonly IPartsService _partsService;

        public PartsServiceTests()
        {
            _partsService = new PartsService();
        }

        /// <summary>
        /// Test case to verify that the initial list of parts contains exactly three items.
        /// </summary>
        [Fact]
        public void GetAllParts_ShouldReturnInitialParts()
        {
            // Arrange (Fact)
            // Already done in the constructor.

            // Act
            var parts = _partsService.GetAllParts();

            // Assert
            Assert.Equal(3, parts.Count());
        }

        /// <summary>
        /// Test case to verify that adding a new part increases the total count of parts.
        /// </summary>
        [Fact]
        public void AddPart_ShouldIncreasePartsCount()
        {
            // Arrange
            var newPart = new Part { Description = "New Part", Price = 10.99M, Quantity = 10 };

            // Act
            _partsService.AddPart(newPart);
            var parts = _partsService.GetAllParts();

            // Assert
            Assert.Equal(4, parts.Count());
        }

        /// <summary>
        /// Test case to verify that the new part added has the correct details.
        /// </summary>
        [Fact]
        public void AddPart_ShouldAddCorrectPartDetails()
        {
            // Arrange
            var newPart = new Part { Description = "New Part", Price = 10.99M, Quantity = 10 };

            // Act
            var addedPart = _partsService.AddPart(newPart);

            // Assert
            Assert.Equal("New Part", addedPart.Description);
            Assert.Equal(10.99M, addedPart.Price);
            Assert.Equal(10, addedPart.Quantity);
        }
    }
}
