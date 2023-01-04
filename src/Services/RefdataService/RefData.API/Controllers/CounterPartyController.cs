using Microsoft.AspNetCore.Mvc;
using RefData.API.Data;
using RefData.API.Models;

namespace RefData.API.Controllers
{
    [Route("api/refdata/[controller]")]
    [ApiController]
    public class CounterPartyController : ControllerBase
    {
        private readonly IRepository<CounterParty> _repo;
        private readonly ILogger<CounterParty> _logger;

        public CounterPartyController(IRepository<CounterParty> repo, ILogger<CounterParty> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult<CounterParty> CreateCounterParty(CounterParty counterParty)
        {
            try
            {
                _repo.Add(counterParty);

                return CreatedAtRoute(nameof(GetCounterPartyById), new { counterParty.Id }, counterParty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create  {ex.Message} ", ex);
                return StatusCode(500);
            }

        }
        [HttpGet("{id}", Name = "GetCounterPartyById")]
        public ActionResult<IEnumerable<CounterParty>> GetCounterPartyById(string id)
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
                _logger.LogError($"Error in GetCounterPartyById {ex.Message} ", ex);
            }
            return NotFound();


        }
        [HttpGet]
        public ActionResult<IEnumerable<CounterParty>> GetAlCounterParty()
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
                _logger.LogError($"Error in GetAlCounterParty {ex.Message} ", ex);
            }
            return NotFound();
        }


    }
}
