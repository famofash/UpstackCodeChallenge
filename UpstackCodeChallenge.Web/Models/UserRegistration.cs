using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpstackCodeChallenge.Web.Models
{
    public class UserRegistration
    {
        [Required]
        public string Email { get; set; }
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        public string ClientURL { get; set; }
        public bool IsVerified { get; set; }
        public string Token { get; set; }
    }
}