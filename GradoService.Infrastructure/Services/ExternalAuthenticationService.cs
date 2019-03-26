using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GradoService.Application.ConfigurationModels;
using GradoService.Application.Interfaces;
using GradoService.Application.Model;
using GradoService.Infrastructure.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace GradoService.Infrastructure.Services
{
    public class ExternalAuthenticationService : IAuthenticationService
    {
        private readonly IOptions<ExternalAuthenticationConfig> _externalAuthApiConfig;
        private readonly HttpClient _httpClient;

        public ExternalAuthenticationService(IOptions<ExternalAuthenticationConfig> externalAuthApiConfig, HttpClient httpClient)
        {
            _externalAuthApiConfig = externalAuthApiConfig;
            _httpClient = httpClient;
        }

        public async Task<AuthenticatedResultModel> IsAuthenticatedAsync(string token)
        {
            var query = _externalAuthApiConfig.Value.AuthApiUrl
                        + _externalAuthApiConfig.Value.AuthenticationStatusPath
                        + "?token=" + token;

            var response = await _httpClient.GetStringAsync(query);

            var jObject = JObject.Parse(response);

            return new AuthenticatedResultModel()
            {
                Authenticated = jObject["authenticated"].Value<bool>()
            };
        }

        public async Task<AuthorizeResultModel> AuthenticateAsync(string login, string password)
        {
            var query = _externalAuthApiConfig.Value.AuthApiUrl
                 + _externalAuthApiConfig.Value.AuthenticationPath;

            var queryData = new JObject
            {
                ["login"] = login,
                ["password"] = password
            };

            var httpContent = new StringContent(queryData.ToString(), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(query, httpContent);

            return HandleAuthenticateResponse(response);
        }

        public async Task<AuthorizeResultModel> RefreshTokenAsync(string refreshToken)
        {
            var query = _externalAuthApiConfig.Value.AuthApiUrl
                        + _externalAuthApiConfig.Value.RefreshTokenPath
                        + "?refreshToken=" + refreshToken;

            var response = await _httpClient.GetAsync(query);

            return HandleAuthenticateResponse(response);
        }

        private AuthorizeResultModel HandleAuthenticateResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new InvalidAuthenticationException();
            }

            var responseJObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            return new AuthorizeResultModel()
            {
                Token = responseJObject["token"].Value<string>(),
                RefreshToken = responseJObject["refreshToken"].Value<string>(),
                LiveTime = responseJObject["ttl"].Value<int>()
            };
        }
    }
}
