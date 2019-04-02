using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradoService.Application.Exceptions
{
    /// <summary>
    /// Custom Exception class that knows about HTTP 
    /// result codes and includes a validation errors
    /// error collection that can optionally be set with
    /// multiple errors.
    /// </summary>
    public class ApiException : System.Exception
    {
        public int StatusCode { get; set; }

        public ApiException(string message, int statusCode = 500)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(System.Exception ex, int statusCode = 500)
            : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
