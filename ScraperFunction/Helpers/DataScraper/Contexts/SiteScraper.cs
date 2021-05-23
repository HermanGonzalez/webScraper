using HtmlAgilityPack;
using Microsoft.ServiceBus.Messaging;
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
        private readonly EventHubClient _eventHubClient;


        public SiteScraper(IHtmlParser parser, ServiceConfiguration config) {
            _page = config.ScrapPageUrl;
            _firstPage = config.LandingPage;
            _htmlParser = parser;
            _eventHubClient = EventHubClient.CreateFromConnectionString(config.EventHubConnectionString);
        }
        
        
        public async Task ScrapData()
        {
            await ProcessDocument(_firstPage);
        }

        private async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();

            var response = client.GetStringAsync(fullUrl);

            return await response;
        }

        private async Task ProcessDocument(string currentPage) {
            var stringPage = await CallUrl($"{_page}/{currentPage}");
            var htmlDocument = _htmlParser.Parse(stringPage);


            var nextPage = htmlDocument.GetNextPage();

            foreach (var item in htmlDocument.ParseHtmlDocumentToBooks())
                await _eventHubClient.SendAsync(item);
            

            if (!string.IsNullOrEmpty(nextPage))
                await ProcessDocument(nextPage);
        }
    }
}
