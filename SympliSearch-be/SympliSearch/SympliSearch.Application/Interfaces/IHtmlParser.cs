namespace SympliSearch.Application.Interfaces
{
    public interface IHtmlParser
    {
        List<int> FindUrlPositionsForGoogle(string htmlContent, string targetUrl);
        List<int> FindUrlPositionsForBing(string htmlContent, string targetUrl);
    }
}
