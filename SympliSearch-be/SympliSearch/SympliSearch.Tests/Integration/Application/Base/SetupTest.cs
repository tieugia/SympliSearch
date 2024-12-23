using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Infrastructure.Http;
using SympliSearch.Infrastructure.Parsers;
using SympliSearch.Infrastructure.Services;

namespace SympliSearch.Tests.Integration.Application.Base
{
    public abstract class SetupTest
    {
        protected ServiceProvider _serviceProvider = null!;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Shared services           
            services.AddScoped<IHtmlParser, HtmlParser>();
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddHttpClient();
            services.AddScoped<IHttpClient, HttpClientWrapper>();
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
