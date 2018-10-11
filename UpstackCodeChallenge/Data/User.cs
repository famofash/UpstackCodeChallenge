using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Data
{
    [Table("Tbl_UserRegistration")]
    public class User
    {
        public int ID { get; set; }
       
        public string Email { get; set; }
        public string Username { get; set; }
        public string ClientURL { get; set; } 
        public string Token { get; set; }
        public bool IsVerified { get; set; }
    }
}
