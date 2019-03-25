using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GradoService.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GradoService.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidAuthenticationException)
            {
                context.HttpContext.Response.ContentType = "text/plain";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                context.Result = new ContentResult { Content = context.Exception.Message };

                return;
            }
        }
    }
}
