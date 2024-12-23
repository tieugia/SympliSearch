using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Infrastructure.Services;
using SympliSearch.Tests.Constants;
using SympliSearch.Tests.Integration.Infrastructure.Base;

namespace SympliSearch.Tests.Integration.Infrastructure
{
    [TestClass]
    public class GoogleSearchServiceTests : SetupTest
    {
        protected override void RegisterTestSpecificServices(IServiceCollection services)
        {
            services.AddScoped<IGoogleSearchService, GoogleSearchService>();
        }

        [TestMethod]
        public async Task SearchAsync_ShouldReturnEmptyPositions_ForNonExistentUrl()
        {
            // Arrange
            var googleSearchService = _serviceProvider.GetRequiredService<IGoogleSearchService>();
            var keywords = TestConstants.Keyword;
            var url = "https://anlacphat.com";

            // Act
            var response = await googleSearchService.SearchAsync(keywords, url);

            // Assert
            response.Should().NotBeNull();
            response.Positions.Should().BeEmpty();
        }

        [TestMethod]
        public async Task SearchAsync_ShouldHandleEmptyKeywords()
        {
            // Arrange
            var googleSearchService = _serviceProvider.GetRequiredService<IGoogleSearchService>();
            var keywords = string.Empty;
            var url = TestConstants.Url;

            // Act
            var response = await googleSearchService.SearchAsync(keywords, url);

            // Assert
            response.Should().NotBeNull();
            response.Positions.Should().BeEmpty();
        }

        [TestMethod]
        public async Task SearchAsync_ShouldHandleSpecialCharactersInKeywords()
        {
            // Arrange
            var googleSearchService = _serviceProvider.GetRequiredService<IGoogleSearchService>();
            var keywords = "@#$%^&*()_+<>?{}|~";
            var url = TestConstants.Url;

            // Act
            var response = await googleSearchService.SearchAsync(keywords, url);

            // Assert
            response.Should().NotBeNull();
            response.Positions.Should().BeEmpty();
        }

        [TestMethod]
        public async Task SearchAsync_ShouldReturnPositions_ForLongKeywords()
        {
            // Arrange
            var googleSearchService = _serviceProvider.GetRequiredService<IGoogleSearchService>();
            var keywords = new string('a', 500); // Very long keyword
            var url = TestConstants.Url;

            // Act
            var response = await googleSearchService.SearchAsync(keywords, url);

            // Assert
            response.Should().NotBeNull();
            response.Positions.Should().BeEmpty();
        }
    }
}
