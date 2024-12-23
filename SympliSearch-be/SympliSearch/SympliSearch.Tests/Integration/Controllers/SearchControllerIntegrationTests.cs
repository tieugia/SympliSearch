using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SympliSearch.Application.Queries.Common;
using SympliSearch.Tests.Constants;
using System.Net;
using System.Net.Http.Json;

namespace SympliSearch.Tests.Integration.Controllers
{
    [TestClass]
    public class SearchControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public SearchControllerIntegrationTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task GetRanksFromGoogle_ShouldReturnSuccessWithValidResponse()
        {
            // Act
            var response = await _client.GetAsync(
                $"/api/search/google?keywords={TestConstants.Keyword}&url={TestConstants.Url}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<RankingResponseDto>();
            content.Should().NotBeNull();
            content!.Positions.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task GetRanksFromBing_ShouldReturnSuccessWithValidResponse()
        {
            // Act
            var response = await _client.GetAsync(
                $"/api/search/bing?keywords={TestConstants.Keyword}&url={TestConstants.Url}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadFromJsonAsync<RankingResponseDto>();
            content.Should().NotBeNull();
            content!.Positions.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task GetRanksFromGoogle_ShouldReturnBadRequest_WhenKeywordsAreMissing()
        {
            // Arrange
            var query = new { Url = TestConstants.Url };

            // Act
            var response = await _client.GetAsync($"/api/search/google?url={query.Url}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task GetRanksFromBing_ShouldReturnBadRequest_WhenUrlIsMissing()
        {
            // Arrange
            var query = new { Keywords = TestConstants.Keyword };

            // Act
            var response = await _client.GetAsync($"/api/search/bing?keywords={query.Keywords}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
