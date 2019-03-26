using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradoService.Application.Interfaces;
using GradoService.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace GradoService.WebUI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        // GET: /api/auth
        [HttpGet]
        public async Task<JsonResult> Get([FromQuery] string token)
        {
            var authenticatedResult = await _authenticationService.IsAuthenticatedAsync(token);

            return new JsonResult(authenticatedResult);

        }

        // POST: /api/auth/token
        [HttpPost("token")]
        public async Task<JsonResult> Post([FromBody] LoginModel loginModel)
        {
            var authorizeResult = await _authenticationService.AuthenticateAsync(loginModel.Login, loginModel.Password);

            return new JsonResult(authorizeResult);
        }

        // GET: /api/auth/token/refresh
        [HttpGet("token/refresh")]
        public async Task<JsonResult> GetRefreshToken([FromQuery] string refreshToken)
        {
            var refreshTokenResult = await _authenticationService.RefreshTokenAsync(refreshToken);

            return new JsonResult(refreshTokenResult);
        }

        // GET: /api/user/current
        [HttpGet("/api/users/current")]
        public JsonResult GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        // POST: /api/logout
        [HttpPost("/api/logout")]
        public JsonResult Logout([FromQuery] string token)
        {
            throw new NotImplementedException();
        }
    }
}
