using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpstackCodeChallenge.Contracts;
using UpstackCodeChallenge.Models;
using UpstackCodeChallenge.Data;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace UpstackCodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUnitOfWork _UnitOfWork; private SendGridModel _sendgrid;
        public UserController(IUnitOfWork UnitofWork , IOptions<SendGridModel> sendgrid)
        {
            _UnitOfWork = UnitofWork;
            _sendgrid = sendgrid.Value;
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] UserBindingModel user)
        {

            if (ModelState.IsValid)
            {
                var UserModel = Mapper.Map<User>(user);
                UserModel.IsVerified = false;
                await _UnitOfWork.UserRepository.InsertAsync(UserModel);
                 _UnitOfWork.Save();
                MailMessage mail = new MailMessage();

                mail.To.Add(user.Email);
                mail.From = new MailAddress(_sendgrid.SMTPUser);
                mail.Subject = "Upstack Registration Verification";
                mail.IsBodyHtml = true;
                mail.Body = "<div> You above link to confirm your email <br/> " +
                            "<a href=" + UserModel.ClientURL +"> Click here to cofirm your account</a> </div>";

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
                var ErrorList = new List<string>();
                foreach(var state in ModelState)
                {
                    foreach(var error in state.Value.Errors)
                    {
                        ErrorList.Add(error.ErrorMessage);
                    }
}
                return BadRequest(ErrorList);
            }
        }
       
        [HttpPut("{token}")]
        public  IActionResult Put([FromRoute] string token)
        {
            if (ModelState.IsValid)
            {
                var UserList = _UnitOfWork.UserRepository.Query().Where(a => a.Token == token).SingleOrDefault();
                if(UserList != null)
                {
                    UserList.IsVerified = true;
                    _UnitOfWork.UserRepository.Update(UserList);
                    _UnitOfWork.Save();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                var ErrorList = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        ErrorList.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(ErrorList);
            }
        }

    }
}