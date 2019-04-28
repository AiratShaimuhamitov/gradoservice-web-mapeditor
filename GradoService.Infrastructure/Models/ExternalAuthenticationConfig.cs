using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradoService.Infrastructure.Models
{
    public class ExternalAuthenticationConfig
    {
        public string AuthApiUrl { get; set; }
        
        public string AuthenticationStatusPath { get; set; }

        public string RefreshTokenPath { get; set; }

        public string AuthenticationPath { get; set; }
    }
}
