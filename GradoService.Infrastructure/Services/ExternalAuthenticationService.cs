using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GradoService.Infrastructure.Models;
using GradoService.Application.Exceptions;
using GradoService.Application.Interfaces;
using GradoService.Application.Model;
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

            var response = await _httpClient.GetAsync(query);

            HandleResponseExceptions(response);

            var jObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

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

            return GetAuthorizeModelFromResponse(response);
        }

        public async Task<AuthorizeResultModel> RefreshTokenAsync(string refreshToken)
        {
            var query = _externalAuthApiConfig.Value.AuthApiUrl
                        + _externalAuthApiConfig.Value.RefreshTokenPath
                        + "?refreshToken=" + refreshToken;

            var response = await _httpClient.GetAsync(query);

            return GetAuthorizeModelFromResponse(response);
        }

        private AuthorizeResultModel GetAuthorizeModelFromResponse(HttpResponseMessage response)
        {
            HandleResponseExceptions(response);

            var responseJObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            return new AuthorizeResultModel()
            {
                Token = responseJObject["token"].Value<string>(),
                RefreshToken = responseJObject["refreshToken"].Value<string>(),
                LiveTime = responseJObject["ttl"].Value<int>()
            };
        }

        /// <summary>
        /// Look at response http status and if it is not OK threw an exception
        /// </summary>
        /// <param name="response">Http response</param>
        private void HandleResponseExceptions(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException(response.Content.ReadAsStringAsync().Result, (int) response.StatusCode);
            }
        }
    }
}
