using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GradoService.Application.Model;

namespace GradoService.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedResultModel> IsAuthenticatedAsync(string token);

        Task<AuthorizeResultModel> AuthenticateAsync(string login, string password);

        Task<AuthorizeResultModel> RefreshTokenAsync(string refreshToken);
    } 
}
