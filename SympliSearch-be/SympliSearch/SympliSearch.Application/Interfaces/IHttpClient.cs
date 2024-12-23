namespace SympliSearch.Application.Interfaces;

public interface IHttpClient
{
    Task<string> GetStringAsync(string requestUri);
    void CleanHeader();
    void AddHeader(string headerName, string headerValue);
}