using SympliSearch.Application.Interfaces;
using SympliSearch.Domain.Constants;
using System.Text.RegularExpressions;

namespace SympliSearch.Infrastructure.Parsers
{
    public class HtmlParser : IHtmlParser
    {
        public List<int> FindUrlPositionsForGoogle(string htmlContent, string targetUrl)
        {
            var positions = new List<int>();
            var matches = GetRegexMatches(htmlContent, CommonConstants.GooglePattern);

            if (matches.Any())
                targetUrl = NormalizeUrl(targetUrl);
            else
                matches = GetRegexMatches(htmlContent, CommonConstants.CiteTagPattern);


            for (int i = 0; i < matches.Count; i++)
            {
                var extractedText = matches[i].Groups[1].Value.Trim();

                if (!string.IsNullOrWhiteSpace(extractedText) && extractedText.Contains(targetUrl, StringComparison.OrdinalIgnoreCase))
                {
                    positions.Add(i + 1);
                }
            }

            return positions;
        }

        public List<int> FindUrlPositionsForBing(string htmlContent, string targetUrl)
        {
            var positions = new List<int>();
            var matches = GetRegexMatches(htmlContent, CommonConstants.BingPattern);

            targetUrl = NormalizeUrl(targetUrl);

            for (int i = 0; i < matches.Count; i++)
            {
                var extractedText = matches[i].Groups[1].Value.Trim();
                var extractedHref = ExtractHrefValue(extractedText);

                if (!string.IsNullOrWhiteSpace(extractedHref) && extractedHref.Contains(targetUrl, StringComparison.OrdinalIgnoreCase))
                {
                    positions.Add(i + 1);
                }
            }

            return positions;
        }

        private MatchCollection GetRegexMatches(string htmlContent, string regexPattern)
        {
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Matches(htmlContent);
        }

        private string? ExtractHrefValue(string htmlContent)
        {
            var regexPattern = @"href=""(https?:\/\/[^""]+)""";
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(htmlContent);

            if (match.Success && match.Groups.Count > 1)
            {
                return NormalizeUrl(match.Groups[1].Value);
            }

            return null;
        }

        private string NormalizeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            return url.Replace("https://", "")
                      .Replace("http://", "")
                      .Trim();
        }
    }
}
