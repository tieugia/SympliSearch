namespace SympliSearch.Domain.Constants
{
    public static class CommonConstants
    {
        public const string GoogleUrl = "https://www.google.com/search";
        public const string BingUrl = "https://www.bing.com/search";
        public const string GooglePattern = @"<div class=""BNeawe UPmit AP7Wnd.*?"">(.*?)<\/div>";
        public const string BingPattern = @"<li class=""b_algo.*?"">(.*?)<\/li>";
        public const int TotalResults = 100;
        public const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36";

        public static string CiteTagPattern = @"<div[^>]*class=[""']notranslate[^""']*[""'][^>]*>.*?<cite[^>]*>(.*?)<\/cite>";
    }
}
