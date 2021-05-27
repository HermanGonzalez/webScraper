using DataServeFunction;
using DataServeFunction.Bootstrap;
using DataServeFunction.Configuration;
using DataServeFunction.Ports.Database;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DataServeFunction
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
                   .AddSingleton<ServiceLogicRoot>()
                   .AddSingleton(provider => ServiceLogicRootFrom(provider).Token)
                   .AddSingleton<BooksStorageContext>()
                   .AddSingleton<BooksRepository>();
        }

        private static void ServiceConfiguration(NullableServiceConfiguration serviceConfiguration, IConfiguration configuration)
        {
            configuration.Bind(serviceConfiguration);
        }

        private static ServiceLogicRoot ServiceLogicRootFrom(IServiceProvider provider)
        {
            return provider.GetRequiredService<ServiceLogicRoot>();
        }
    }
}
