using SympliSearch.Application.Interfaces;
using SympliSearch.Domain.Constants;

namespace SympliSearch.Infrastructure.Http;

public class HttpClientWrapper : IHttpClient
{
    private readonly HttpClient _httpClient;

    public HttpClientWrapper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetStringAsync(string requestUri)
    {
        CleanHeader();
        AddHeader("User-Agent", CommonConstants.DefaultUserAgent);
        return await _httpClient.GetStringAsync(requestUri);
    }

    public void CleanHeader()
    {
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public void AddHeader(string headerName, string headerValue)
    {
        _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
    }
}