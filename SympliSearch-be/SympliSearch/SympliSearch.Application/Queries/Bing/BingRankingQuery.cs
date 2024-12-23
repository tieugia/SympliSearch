using MediatR;
using SympliSearch.Application.Queries.Common;

namespace SympliSearch.Application.Queries.Bing
{
    public class BingRankingQuery : IRequest<RankingResponseDto>
    {
        public string Keywords { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
