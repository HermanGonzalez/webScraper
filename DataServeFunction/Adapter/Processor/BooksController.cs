using DataServeFunction.Authorization;
using DataServeFunction.Ports.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServeFunction.Adapter.Controllers
{
    public class BooksController
    {
        private readonly Token _token;
        private readonly ILogger<BooksController> _logger;
        private readonly BooksRepository _repo;

        public BooksController(Token token, BooksRepository repo, ILogger<BooksController> logger) 
        {
            _token = token;
            _logger = logger;
            _repo = repo;
        }

        [FunctionName("books")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, CancellationToken cancellationToken)
        {
            try
            {
                return await _token.GetFrom(req)
                    .MatchAsync(requestorDto => _repo.GetAll(),
                        Fail: UnauthorizedResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("GController exception ", ex);
                throw;
            }
        }

        private UnauthorizedResult UnauthorizedResult(Exception error)
        {
            _logger.LogWarning("User was unauthorized due to following reason: {error}", error);
            return new UnauthorizedResult();
        }
    }
}
