﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Models
{
    public class SendGridModel
    {
        public string SMTPHost { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPPort { get; set; }
    }
}
