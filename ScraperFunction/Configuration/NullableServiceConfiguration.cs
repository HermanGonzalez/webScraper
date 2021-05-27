using System.Reflection;

namespace ScraperFunction.Configuration
{
    public class NullableServiceConfiguration
    {
        public string? ScrapPageUrl { get; set; }
        public string? LandingPage { get; set; }
        public string? EventHubName { get; set; }
        public string? EventHubConnectionString { get; set; }
        
        public ServiceConfiguration Validate()
        {
            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(this);
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ServiceConfigurationException($"AppSetting Scraper:{pi.Name} is not set");
                    
                }
            }

            return new ServiceConfiguration(
                ScrapPageUrl, 
                LandingPage,
                EventHubName,
                EventHubConnectionString);
        }
    }
}
