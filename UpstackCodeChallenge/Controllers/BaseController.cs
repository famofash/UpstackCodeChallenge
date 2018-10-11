using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UpstackCodeChallenge.Contracts;
using UpstackCodeChallenge.Models;

namespace UpstackCodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController  : ControllerBase
    {
        private SendGridModel _sendgrid;
        public SendEmailController(IOptions<SendGridModel> sendgrid)
        {
            _sendgrid = sendgrid.Value;
        }
       
       
        public IActionResult Post([FromQuery] string email, [FromQuery] string baseUrl = "")
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();

                mail.To.Add(email);
                mail.From = new MailAddress("famofash@gmail.com");
                mail.Subject = "Upstack Registration Verification";
                mail.IsBodyHtml = true;
                mail.Body = "<div> You above link to confirm your email <br/> " +
                            "<a href=" + baseUrl + "/email=" + email + "> Click here to cofirm your account</a> </div>";

                using (SmtpClient SmtpServer = new SmtpClient(_sendgrid.SMTPHost, Convert.ToInt16(_sendgrid.SMTPPort)))
                {
                    Object state = mail;

                    //SmtpServer.EnableSsl = true; ;

                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(_sendgrid.SMTPUser, _sendgrid.SMTPPassword);
                    SmtpServer.EnableSsl = true;


                    SmtpServer.Send(mail);
                }
                   
                return Ok();

            }
            else
            {
                var errors = new List<APIError>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new APIError
                        {
                            Detail = error.ErrorMessage
                        });
                    }
                }
                return BadRequest(errors);
            }
        }


    }
}