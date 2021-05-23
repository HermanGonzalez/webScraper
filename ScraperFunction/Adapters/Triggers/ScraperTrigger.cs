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
        public ScraperTrigger(IWebScraper pageScraper) 
        {
            PageScraper = pageScraper;
        }
        
        [FunctionName("ScraperTimerTrigger")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            await PageScraper.ScrapData();
        }
    }
}
