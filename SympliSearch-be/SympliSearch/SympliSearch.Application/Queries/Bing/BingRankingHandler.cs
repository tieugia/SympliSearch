using MediatR;
using SympliSearch.Application.Interfaces;
using SympliSearch.Application.Queries.Bing;
using SympliSearch.Application.Queries.Common;

public class BingRankingHandler : IRequestHandler<BingRankingQuery, RankingResponseDto>
{
    private readonly IBingSearchService _bingSearchService;
    private readonly ICacheService _cacheService;

    public BingRankingHandler(IBingSearchService bingSearchService, ICacheService cacheService)
    {
        _bingSearchService = bingSearchService;
        _cacheService = cacheService;
    }

    public async Task<RankingResponseDto> Handle(BingRankingQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Bing_{request.Keywords}_{request.Url}".ToLowerInvariant(); 
        if (_cacheService.TryGet(cacheKey, out List<int>? cachedPositions))
        {
            return new RankingResponseDto { Positions = cachedPositions };
        }
        
        var resultsPerpage = 10;

        var response = await _bingSearchService.SearchAsync(request.Keywords, request.Url, resultsPerpage);
        _cacheService.Set(cacheKey, response.Positions, TimeSpan.FromHours(1));

        return new RankingResponseDto { Positions = response.Positions };
    }
}
