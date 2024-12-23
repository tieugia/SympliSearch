using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Infrastructure.Http;
using SympliSearch.Infrastructure.Parsers;

namespace SympliSearch.Tests.Integration.Infrastructure.Base
{
    public abstract class SetupTest
    {
        protected ServiceProvider _serviceProvider = null!;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Shared services           
            services.AddHttpClient();
            services.AddScoped<IHttpClient, HttpClientWrapper>();
            services.AddScoped<IHtmlParser, HtmlParser>();

            // Conditional registrations
            RegisterTestSpecificServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        protected virtual void RegisterTestSpecificServices(IServiceCollection services)
        {
            // To be overridden in derived classes
        }
    }
}
