using SympliSearch.Application.Interfaces;
using SympliSearch.Domain.Constants;
using SympliSearch.Domain.Entities;

namespace SympliSearch.Infrastructure.Services
{
    public class BingSearchService : IBingSearchService
    {
        private readonly IHttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;

        public BingSearchService(IHttpClient httpClient, IHtmlParser htmlParser)
        {
            _httpClient = httpClient;
            _htmlParser = htmlParser;
        }

        public async Task<RankingResponse> SearchAsync(string keywords, string url, int resultsPerPage)
        {
            var positions = new List<int>();
            int totalPages = CommonConstants.TotalResults / resultsPerPage;

            for (int page = 0; page < totalPages; page++)
            {
                int offset = page * resultsPerPage + 1;
                var queryUrl = $"{CommonConstants.BingUrl}?q={Uri.EscapeDataString(keywords)}&first={offset}";

                var htmlContent = await _httpClient.GetStringAsync(queryUrl);

                var pagePositions = _htmlParser.FindUrlPositionsForBing(htmlContent, url);
                positions.AddRange(pagePositions.Select(p => p + offset - 1));
            }

            return new RankingResponse { Positions = positions };
        }
    }
}
