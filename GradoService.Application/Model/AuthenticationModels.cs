using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Model
{
    public class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }


    public class AuthorizeResultModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public int LiveTime { get; set; }
    }

    public class AuthenticatedResultModel
    {
        public bool Authenticated { get; set; }
    }
}
