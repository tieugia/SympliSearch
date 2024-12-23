using MediatR;
using SympliSearch.Application.Interfaces;
using SympliSearch.Application.Queries.Common;
using SympliSearch.Application.Queries.Google;

public class GoogleRankingHandler : IRequestHandler<GoogleRankingQuery, RankingResponseDto>
{
    private readonly IGoogleSearchService _googleSearchService;
    private readonly ICacheService _cacheService;

    public GoogleRankingHandler(IGoogleSearchService googleSearchService, ICacheService cacheService)
    {
        _googleSearchService = googleSearchService;
        _cacheService = cacheService;
    }

    public async Task<RankingResponseDto> Handle(GoogleRankingQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Google_{request.Keywords}_{request.Url}".ToLowerInvariant();
        if (_cacheService.TryGet(cacheKey, out List<int>? cachedPositions))
        {
            return new RankingResponseDto { Positions = cachedPositions };
        }

        var response = await _googleSearchService.SearchAsync(request.Keywords, request.Url);
        _cacheService.Set(cacheKey, response.Positions, TimeSpan.FromHours(1));

        return new RankingResponseDto { Positions = response.Positions };
    }
}
