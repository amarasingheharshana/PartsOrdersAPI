using PartsOrdersAPI.Interfaces;
using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Services
{
    /// <summary>
    /// Service to manage parts, including fetching all parts and adding new parts.
    /// Parts are stored in memory for simplicity.
    /// </summary>
    public class PartsService : IPartsService
    {
        private readonly List<Part> _parts = new List<Part>
    {
        new Part { Id = 1, Description = "Wire", Price = 5.99M, Quantity = 5 },
        new Part { Id = 2, Description = "Brake Fluid", Price = 4.90M, Quantity = 20 },
        new Part { Id = 3, Description = "Engine Oil", Price = 15.00M, Quantity = 12 }
    };

        /// <summary>
        /// Retrieves all parts from the in-memory list.
        /// </summary>
        /// <returns>A collection of all parts.</returns>
        public IEnumerable<Part> GetAllParts() => _parts;

        /// <summary>
        /// Adds a new part to the in-memory list of parts.
        /// The new part is assigned a unique ID based on the highest existing ID.
        /// </summary>
        /// <param name="part">The part to be added.</param>
        /// <returns>The added part with its assigned ID.</returns>
        public Part AddPart(Part part)
        {
            part.Id = _parts.Max(p => p.Id) + 1;
            _parts.Add(part);
            return part;
        }

        /// <summary>
        /// Retrieves a part by its ID.
        /// </summary>
        /// <param name="id">The ID of the part to retrieve.</param>
        /// <returns>The part with the specified ID, or null if no part with that ID exists.</returns>
        public Part GetPartById(int id) => _parts.FirstOrDefault(p => p.Id == id);
    }
}
