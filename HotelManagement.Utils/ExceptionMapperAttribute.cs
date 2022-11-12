using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Utils
{
    public class ExceptionMapperAttribute:Attribute,IExceptionFilter
    {

            private readonly ILogger<ExceptionMapperAttribute> logger;
            public Type ExceptionType { get; set; }
            public int StatusCode { get; set; }

            public string Message { get; set; }
            public bool IncludeExceptionMessage { get; set; } = true;

           

            public void OnException(ExceptionContext context)
            {
                //this works like a catch block
                if (context.Exception.GetType() == ExceptionType)
                {
                    context.HttpContext.Response.StatusCode = StatusCode;
                    string message = "";
                    if (string.IsNullOrEmpty(Message))
                    {
                        message = Message;
                       // logger.LogError(message);
                    }
                    else if (IncludeExceptionMessage)
                    {
                        message = context.Exception.Message;
                        // logger.LogError(message);
                    }
                    else
                    {
                        message = "Some Error Occured";
                        // logger.LogError(message);
                    }
                    context.Result = new JsonResult(new
                    {
                        Status = StatusCode,
                        Message = message
                    });
                }
            }
        }
    
}
