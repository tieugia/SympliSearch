using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Infrastructure.Http;
using SympliSearch.Infrastructure.Parsers;
using SympliSearch.Infrastructure.Services;

namespace SympliSearch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            // Register caching services
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            // Register search services
            services.AddScoped<IGoogleSearchService, GoogleSearchService>();
            services.AddScoped<IBingSearchService, BingSearchService>();

            // Register parsers
            services.AddScoped<IHtmlParser, HtmlParser>();

            // Register HttpClientFactory
            services.AddHttpClient();
            services.AddScoped<IHttpClient, HttpClientWrapper>();

            return services;
        }
    }
}
