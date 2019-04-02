using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradoService.Application.Exceptions
{
    /// <summary>
    /// An API Error response returned to the client
    /// </summary>
    public class ApiError
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Detail { get; set; }
        public object Data { get; set; }

        public ApiError(string message)
        {
            Message = message;
            IsError = true;
        }
    }
}
