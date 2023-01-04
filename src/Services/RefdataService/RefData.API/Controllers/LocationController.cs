using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefData.API.Data;
using RefData.API.Models;

namespace RefData.API.Controllers
{
    [Authorize]
    [Route("api/refdata/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IRepository<Location> _repo;
        private readonly ILogger<Location> _logger;
        public LocationController(IRepository<Location> repo, ILogger<Location> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult<Location> CreateLocation(Location location)
        {
            try
            {
                _repo.Add(location);

                return CreatedAtRoute(nameof(GetLocationById), new { location.Id }, location);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllLocation {ex.Message} ", ex);
                return StatusCode(500);
            }

        }
        [HttpGet("{id}", Name = "GetLocationById")]
        public ActionResult<IEnumerable<Location>> GetLocationById(string id)
        {
            try
            {
                var platform = _repo.GetByIdAsync(id);

                if (platform != null)
                {
                    return Ok(platform);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLocationById {ex.Message} ", ex);
            }
            return NotFound();


        }
        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetAllLocation()
        {
            try
            {
                var platform = _repo.GetAll();

                if (platform != null)
                {
                    return Ok(platform);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllLocation {ex.Message} ", ex);
            }
            return NotFound();
        }
    }
}
