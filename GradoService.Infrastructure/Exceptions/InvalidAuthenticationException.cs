using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Infrastructure.Exceptions
{
    public class InvalidAuthenticationException : Exception
    {
        public InvalidAuthenticationException()
            : base("Invalid login or password")
        {
            
        }
    }
}
