using SympliSearch.Application.Interfaces;
using SympliSearch.Domain.Constants;
using SympliSearch.Domain.Entities;

namespace SympliSearch.Infrastructure.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IHttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;       

        public GoogleSearchService(IHttpClient httpClient, IHtmlParser htmlParser)
        {
            _httpClient = httpClient;
            _htmlParser = htmlParser;
        }

        public async Task<RankingResponse> SearchAsync(string keywords, string url)
        {
            var queryUrl = $"{CommonConstants.GoogleUrl}?q={Uri.EscapeDataString(keywords)}&num={CommonConstants.TotalResults}";

            var htmlContent = await _httpClient.GetStringAsync(queryUrl);

            var positions = _htmlParser.FindUrlPositionsForGoogle(htmlContent, url);

            return new RankingResponse { Positions = positions };
        }
    }
}
