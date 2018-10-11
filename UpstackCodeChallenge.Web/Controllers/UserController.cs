using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpstackCodeChallenge.Web.Models;
using System.Net;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Web.Controllers
{
    [RoutePrefix("User")]
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("SaveUser")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUser(UserRegistration model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var urlBuilder =
                     new System.UriBuilder(Request.Url.AbsoluteUri)
                     {
                         Path = Url.Action("Activate", "User"),
                         Query = null,
                     };

                    Uri uri = urlBuilder.Uri;
                    
                    model.ClientURL = urlBuilder.ToString() + "/" + CreateMD5(model.Username);
                    model.Token = CreateMD5(model.Username);
                    var client = new HttpClient();
                    //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsJsonAsync("http://localhost:53099/api/User", model).Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        ViewBag.Message = "User Registered Successfully";
                    }


                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }
            return View("Index");
        }



        [HttpGet]
        [Route("Activate/{token}")]
        public ActionResult Activate(string token)
        {
            try
            {

                var client = new HttpClient();
                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.PutAsJsonAsync("http://localhost:53099/api/User/" + token, "").Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.Message = "User Activated Successfully";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }
            return View("Index");
        }
    }
}