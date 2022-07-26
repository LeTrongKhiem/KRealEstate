using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EmailController> _logger;
        private readonly IConfiguration _configuration;
        public EmailController(IEmailSender emailSender, ILogger<EmailController> logger, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string email, string subject, string htmlMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _emailSender.SendEmailAsync(email, subject, htmlMessage);
            return Ok(result);
        }
    }
}
