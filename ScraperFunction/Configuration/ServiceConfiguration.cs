namespace ScraperFunction.Configuration
{
    public class ServiceConfiguration
    {
        public string? ScrapPageUrl { get; set; }
        public string? LandingPage { get; set; }
        public string? EventHubConnectionString { get; set; }
        public ServiceConfiguration(
            string ScrapPageUrl,
            string LandingPage)
        {
            this.ScrapPageUrl = ScrapPageUrl;
            this.LandingPage = LandingPage;
    }
    }
}
