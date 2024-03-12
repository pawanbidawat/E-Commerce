
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using TemporalBoxApi.Configuration;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.Models;

namespace TemporalBoxApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
       
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
          
        }

        [HttpPost]
        [Route("SendMail")]
        public bool SendEmail(EmailData mailData)
        {

            var data = _mailService.SendMail(mailData);
            return data;
        }
    }
}
