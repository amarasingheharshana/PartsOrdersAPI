using Microsoft.AspNetCore.Mvc;
using PartsOrdersAPI.Interfaces;
using PartsOrdersAPI.Models;

namespace PartsOrdersAPI.Controllers
{
    /// <summary>
    /// Controller to handle operations related to parts, such as retrieving all parts and adding new parts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartsService _partsService;
        private readonly ILogger<PartsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartsController"/> class.
        /// </summary>
        /// <param name="partsService">The service used to manage parts.</param>
        public PartsController(IPartsService partsService, ILogger<PartsController> logger)
        {
            _partsService = partsService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all parts available in the inventory.
        /// </summary>
        /// <returns>A list of all parts.</returns>
        /// <response code="200">Returns the list of all parts.</response>
        [HttpGet("get_parts")]
        public ActionResult<IEnumerable<Part>> GetAllParts()
        {
            _logger.LogInformation("Fetching all parts.");
            var parts = _partsService.GetAllParts();
            return Ok(parts);
        }

        /// <summary>
        /// Adds a new part to the inventory.
        /// </summary>
        /// <param name="part">The part to add.</param>
        /// <returns>The added part with its assigned ID.</returns>
        /// <response code="200">Returns the added part with details.</response>
        /// <response code="400">If the part is invalid.</response>
        [HttpPost("add_parts")]
        public ActionResult<Part> AddPart([FromBody] Part part)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid part data received.");
                return BadRequest(ModelState);
            }
            if (part == null || string.IsNullOrWhiteSpace(part.Description) || part.Price <= 0 || part.Quantity < 0)
            {
                return BadRequest("Invalid part details.");
            }

            _logger.LogInformation("Adding new part: {Part}", part.Description);
            var newPart = _partsService.AddPart(part);
            return CreatedAtAction(nameof(GetAllParts), new { id = newPart.Id }, newPart);
        }
    }
}
