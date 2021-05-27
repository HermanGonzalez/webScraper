using DataServeFunction.Authorization;
using Microsoft.Extensions.Logging;

namespace DataServeFunction.Bootstrap
{
    public class ServiceLogicRoot
    {
        public Token Token { get; }

        public ServiceLogicRoot(ILoggerFactory loggerFactory)
        {
            Token = new Token(loggerFactory.CreateLogger<Token>());
        }
    }
}
