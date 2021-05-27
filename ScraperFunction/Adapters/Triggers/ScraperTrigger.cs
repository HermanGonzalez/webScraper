using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ScraperFunction.Helpers.DataScraper;
using System;
using System.Threading.Tasks;

namespace ScraperFunction.Adapters.Triggers
{
    public class ScraperTrigger
    {
        public readonly IWebScraper PageScraper;
        private readonly ILogger<ScraperTrigger> _logger;
        public ScraperTrigger(IWebScraper pageScraper, ILogger<ScraperTrigger> logger) 
        {
            PageScraper = pageScraper;
            _logger = logger;
        }
        
        [FunctionName("ScraperTimerTrigger")]
        public async Task Run([TimerTrigger("0 30 16 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                await PageScraper.ScrapData();
            }
            catch (Exception ex) 
            {
                _logger.LogCritical("ScraperTrigger exception ", ex);

                throw;
            }
        }
    }
}
