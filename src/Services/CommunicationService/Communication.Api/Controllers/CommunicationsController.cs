using Microsoft.AspNetCore.Mvc;

namespace Communications.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationsController : ControllerBase
    {
        [HttpPost]
        [Route("Email")]
        public IActionResult Email()
        {
            return Ok("Successful Queued");
        }
        [HttpPost]
        [Route("SMS")]
        public IActionResult SMS()
        {
            return Ok("Successful Queued");
        }

    }
}
