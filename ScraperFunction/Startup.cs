using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ScraperFunction;
using ScraperFunction.Configuration;
using ScraperFunction.Helpers.DataScraper;
using ScraperFunction.Helpers.DataScraper.Contexts;
using ScraperFunction.Helpers.Parsers;
using ScraperFunction.Helpers.Parsers.Contexts;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ScraperFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder
                 .Services
                   .AddOptions<NullableServiceConfiguration>()
                   .Configure<IConfiguration>(ServiceConfiguration)
                 .Services
                   .AddSingleton(provider =>
                         provider.GetService<IOptions<NullableServiceConfiguration>>()
                         .Value.Validate())
                   .AddSingleton<IHtmlParser, HtmlParser>()
                   .AddSingleton<IWebScraper, SiteScraper>();
        }

        private static void ServiceConfiguration(NullableServiceConfiguration serviceConfiguration, IConfiguration configuration)
        {
            configuration.Bind(serviceConfiguration);
        }
    }
}
