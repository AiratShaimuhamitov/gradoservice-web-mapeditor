using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GradoService.Infrastructure.Exceptions
{
    public class HttpAuthenticationException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public HttpAuthenticationException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
