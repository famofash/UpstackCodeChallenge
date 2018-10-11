using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Models
{
    public class UserBindingModel
    {
    public int ID { get; set; }
      [Required]  public string Email { get; set; }
      [Required]  public string Username { get; set; }
        public string ClientURL { get; set; }
        public string Token { get; set; }
        public bool IsVerified { get; set; }
    }
}
