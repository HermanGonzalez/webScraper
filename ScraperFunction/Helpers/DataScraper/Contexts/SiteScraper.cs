using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Logging;
using ScraperFunction.Configuration;
using ScraperFunction.Helpers.Extensions;
using ScraperFunction.Helpers.Parsers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScraperFunction.Helpers.DataScraper.Contexts
{
    public class SiteScraper : IWebScraper
    {
        private readonly string _page;
        private readonly string _firstPage;
        private readonly IHtmlParser _htmlParser;
        private readonly string _eventHubConnectionString;
        private readonly string _eventHubName;
        private readonly ILogger<SiteScraper> _logger;

        public SiteScraper(
            IHtmlParser parser, 
            ServiceConfiguration config, 
            ILogger<SiteScraper> logger) {
            _page = config.ScrapPageUrl;
            _firstPage = config.LandingPage;
            _htmlParser = parser;
            _eventHubConnectionString = config.EventHubConnectionString;
            _eventHubName = config.EventHubName;
            _logger = logger;
        }
        
        
        public async Task ScrapData()
        {
            await ProcessDocument(_firstPage);
        }

        private async Task<string> CallUrl(string fullUrl)
        {
            try
            {
                _logger.LogInformation("scraping current page: " + fullUrl);
                HttpClient client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                client.DefaultRequestHeaders.Accept.Clear();

                var response = client.GetStringAsync(fullUrl);

                return await response;
            }
            catch (Exception ex) {
                _logger.LogError("fail load page", ex);
                throw;
            }
        }

        private async Task ProcessDocument(string currentPage) {
            try
            {
                _logger.LogInformation("start scraping " + currentPage);

                string nextPageUrl = Url(currentPage);
                
                var stringPage = await CallUrl($"{_page}/{nextPageUrl}");
                var htmlDocument = _htmlParser.Parse(stringPage);


                var nextPage = htmlDocument.GetNextPage();
                await using (var producerClient = new EventHubProducerClient(_eventHubConnectionString))
                {
                    using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                    foreach (var item in htmlDocument.ParseHtmlDocumentToBooks())
                        eventBatch.TryAdd(item);

                    await producerClient.SendAsync(eventBatch);
                }



                if (!string.IsNullOrEmpty(nextPage))
                    await ProcessDocument(nextPage);
            }
            catch (Exception ex) 
            {
                _logger.LogError("fail full scraping routine", ex);
            }
        }

        private string Url(string currentUrl) {
            if (!currentUrl.Contains("catalogue") && !currentUrl.Contains("index"))
                return $"catalogue/{currentUrl}";

           return currentUrl;
        }
    }
}
