using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SympliSearch.Application.Interfaces;
using SympliSearch.Infrastructure.Services;

namespace SympliSearch.Tests.Integration.Infrastructure
{
    [TestClass]
    public class MemoryCacheServiceTests
    {
        private ICacheService _cacheService = null!;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            var provider = services.BuildServiceProvider();
            _cacheService = provider.GetRequiredService<ICacheService>();
        }

        [TestMethod]
        public void SetAndTryGet_ShouldReturnCachedValue()
        {
            // Arrange
            var cacheKey = "test-key";
            int? cacheValue = 100;
            var cacheDuration = TimeSpan.FromMinutes(1);

            // Act
            _cacheService.Set(cacheKey, cacheValue, cacheDuration);

            // Assert
            _cacheService.TryGet<int?>(cacheKey, out var cachedValue).Should().BeTrue();
            cachedValue.Should().NotBeNull();
            cachedValue.Should().Be(cacheValue);
        }

        [TestMethod]
        public void TryGet_NonExistentKey_ReturnsFalse()
        {
            // Act
            _cacheService.TryGet<int?>("non-existent-key", out var cachedValue);

            // Assert
            cachedValue.Should().BeNull();
        }

        [TestMethod]
        public void SetAndTryGet_DifferentTypes_ReturnsCorrectValue()
        {
            // Arrange
            var cacheKey = "test-key";
            var cacheValue = "test-value";
            var cacheDuration = TimeSpan.FromMinutes(1);

            // Act
            _cacheService.Set(cacheKey, cacheValue, cacheDuration);

            // Assert
            _cacheService.TryGet<string>(cacheKey, out var cachedValue).Should().BeTrue();
            cachedValue.Should().NotBeNull();
            cachedValue.Should().Be(cacheValue);
        }

        [TestMethod]
        public void SetAndTryGet_NullValue_ReturnsNull()
        {
            // Arrange
            var cacheKey = "test-key";
            string? cacheValue = null;
            var cacheDuration = TimeSpan.FromMinutes(1);

            // Act
            _cacheService.Set(cacheKey, cacheValue, cacheDuration);

            // Assert
            _cacheService.TryGet<string>(cacheKey, out var cachedValue).Should().BeTrue();
            cachedValue.Should().BeNull();
        }
    }
}