using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using static LanguageExt.Prelude;

namespace DataServeFunction.Authorization
{
    public class Token
    {
        private readonly ILogger<Token> _logger;
        private const string AuthHeaderName = "Authorization";
        private const string BearerPrefix = "Bearer ";

        public Token(ILogger<Token> logger)
        {
            _logger = logger;
        }

        public Try<RequestorDto> GetFrom(HttpRequest request)
        {
            _logger.LogInformation($"Inside Token GetFrom for request {request}");
            try
            {
                return Try(() => Read(TokenFrom(request)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Cannot read token from ", ex);
                throw;
            }
        }

        private static RequestorDto Read(string token)
        {
            return JsonConvert.DeserializeObject<RequestorDto>(new JwtSecurityTokenHandler()
                .ReadJwtToken(token)
                .Payload
                .SerializeToJson());
        }

        private static string TokenFrom(HttpRequest request)
        {
            return request == null ||
                   !request.Headers.ContainsKey(AuthHeaderName) ||
                   !request.Headers[AuthHeaderName].ToString().StartsWith(BearerPrefix)
                ? throw new Exception("T001 Token does not exist.")
                : request.Headers[AuthHeaderName].ToString().Substring(BearerPrefix.Length);
        }
    }
}
