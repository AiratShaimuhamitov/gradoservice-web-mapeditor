using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GradoService.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not found.", (int) HttpStatusCode.NotFound)
        {
        }
    }
}
