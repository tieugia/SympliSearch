using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Application.Queries.Google;
using SympliSearch.Infrastructure.Services;
using SympliSearch.Tests.Constants;
using SympliSearch.Tests.Integration.Application.Base;

namespace SympliSearch.Tests.Integration.Application
{
    [TestClass]
    public class GoogleRankingHandlerTests : SetupTest
    {
        protected override void RegisterTestSpecificServices(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GoogleRankingHandler).Assembly));
            services.AddScoped<IGoogleSearchService, GoogleSearchService>();
        }

        [TestMethod]
        public async Task Handle_GivenValidQuery_ShouldReturnPositions()
        {
            // Arrange
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            var query = new GoogleRankingQuery
            {
                Keywords = TestConstants.Keyword,
                Url = TestConstants.Url
            };

            // Act
            var result = await mediator.Send(query);

            // Assert
            result.Should().NotBeNull();
            result.Positions.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Handle_GivenInvalidUrl_ShouldReturnEmptyPositions()
        {
            // Arrange
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            var query = new GoogleRankingQuery
            {
                Keywords = TestConstants.Keyword,
                Url = "https://anlacphat.com"
            };

            // Act
            var result = await mediator.Send(query);

            // Assert
            result.Should().NotBeNull();
            result.Positions.Should().BeEmpty();
        }
    }
}
