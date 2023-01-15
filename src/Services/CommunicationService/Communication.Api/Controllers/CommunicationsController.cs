using Communications.Api.Model;
using Communications.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Communications.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationsController : ControllerBase
    {
        private readonly IRetryCommunication _retryCommunication;
        public CommunicationsController(IRetryCommunication retryCommunication)
        {
            _retryCommunication = retryCommunication;
        }
        [HttpPost]
        [Route("Email")]
        public async Task<IActionResult> Email(Guid notificationId)
        {
            try
            {
                var emailResponse = await _retryCommunication.RetryEmail(notificationId);
                return Ok(emailResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        [Route("SMS")]
        public async Task<IActionResult> SMS(Guid notificationId)
        {

            try
            {
                var smsResponse = await _retryCommunication.RetrySMS(notificationId);
                return Ok(smsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
