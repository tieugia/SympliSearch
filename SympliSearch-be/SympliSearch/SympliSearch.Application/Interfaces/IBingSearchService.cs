using SympliSearch.Domain.Entities;

namespace SympliSearch.Application.Interfaces
{
    public interface IBingSearchService
    {
        Task<RankingResponse> SearchAsync(string keywords, string url, int resultsPerPage);
    }
}
