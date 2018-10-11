using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpstackCodeChallenge.Models;

namespace UpstackCodeChallenge.Filter
{
    public class ErrorFilter : IExceptionFilter 
    {
        private readonly IHostingEnvironment _env;
        public ErrorFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new APIError();
            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
                context.Result = new ObjectResult(error)
                {
                    StatusCode = 500
                };
            }
            else
            {
                error.Message = "Server Error Occurred";
                error.Detail = context.Exception.Message;
            }


        }
    }
}
