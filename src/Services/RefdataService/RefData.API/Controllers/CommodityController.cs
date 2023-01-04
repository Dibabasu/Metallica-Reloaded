using Microsoft.AspNetCore.Mvc;
using RefData.API.Data;
using RefData.API.Models;

namespace RefData.API.Controllers
{
    [Route("api/refdata/[controller]")]
    [ApiController]
    public class CommodityController : ControllerBase
    {
        private readonly IRepository<Commodity> _repo;
        private readonly ILogger<Commodity> _logger;

        public CommodityController(IRepository<Commodity> repo, ILogger<Commodity> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult<Commodity> CreateCommodity(Commodity commodity)
        {
            try
            {
                _repo.Add(commodity);

                return CreatedAtRoute(nameof(GetCommodityById), new { commodity.Id }, commodity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create  {ex.Message} ", ex);
                return StatusCode(500);
            }

        }
        [HttpGet("{id}", Name = "GetCommodityById")]
        public ActionResult<IEnumerable<Commodity>> GetCommodityById(string id)
        {
            try
            {
                var result = _repo.GetByIdAsync(id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCommodityById {ex.Message} ", ex);
            }
            return NotFound();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Commodity>> GetAllCommodity()
        {
            try
            {
                var result = _repo.GetAll();
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCommodityById {ex.Message} ", ex);
            }
            return NotFound();
        }
    }
}
