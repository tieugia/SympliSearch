using SympliSearch.Domain.Entities;

namespace SympliSearch.Application.Interfaces
{
    public interface IGoogleSearchService
    {
        Task<RankingResponse> SearchAsync(string keywords, string url);
    }
}
