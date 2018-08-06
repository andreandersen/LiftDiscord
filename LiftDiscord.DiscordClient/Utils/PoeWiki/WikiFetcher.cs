using MwParserFromScratch;
using MwParserFromScratch.Nodes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiftDiscord.DiscordClient.Utils.PoeWiki
{
    public static class WikiFetcher
    {
        const string endpointUrl = "https://pathofexile.gamepedia.com/api.php";


        public static async Task<(string, string)> OpenSearchAsync(string query)
        {
            using (var client = new HttpClient())
            {
                var queryUrl = $"{endpointUrl}?action=opensearch&search={query}&suggest=true&redirects=resolve";
                var resultString = await client.GetStringAsync(queryUrl).ConfigureAwait(false);
                var arrResult = JArray.Parse(resultString);
                
                var page = (string)arrResult.Last.First;
                var title = (string)arrResult[1].First;

                return (title, page);
            }
        }

        public static async Task<string> GetWikiPage(string title)
        {
            var requestContent = new Dictionary<string, string>
            {
                { "format", "json" },
                { "action", "query" },
                { "prop", "revisions" },
                { "rvlimit", "1" },
                { "rvprop", "content" },
                { "titles", title },
                { "redirects", "resolve" }
            };

            using (var client = new HttpClient())
            using (var postVars = new FormUrlEncodedContent(requestContent))
            {
                var response = await client.PostAsync(endpointUrl, postVars);
                var stringContent = await response.Content.ReadAsStringAsync();
                var root = JObject.Parse(stringContent);
                var content = (string)root["query"]["pages"].Children<JProperty>().First().Value["revisions"][0]["*"];
                var parser = new WikitextParser();

                var result = parser.Parse(content);

                var lines = result.EnumChildren().ToList();
                var textLines = lines.Select(c => ExtractStringFromQuery(c))
                    .Where(c => c != "\n")
                    .Select(c => c.Trim())
                    .ToList();

                var taken = textLines
                    .SkipWhile(c => String.IsNullOrEmpty(c))
                    .TakeWhile(c => !String.IsNullOrEmpty(c) && !c.StartsWith("**"))
                    .Take(3)
                    .ToList();

                return string.Join("\n", taken);
            }
        }

        static string ExtractStringFromQuery(Node node)
        {
            switch (node)
            {
                case Template t:
                    return t.Arguments.OfType<TemplateArgument>().Select(e => e.Value.ToPlainText()).FirstOrDefault() ?? "";
                case Paragraph p:
                    if (string.IsNullOrEmpty(p.ToPlainText()))
                        return "";

                    var fragments = p.Inlines.Select(c => ExtractStringFromQuery(c)).ToArray();
                    return String.Join("", fragments);
                case PlainText p:
                    return p.Content;
                case WikiLink l:
                    if (l.Target.ToString().Contains(":"))
                        return "";
                    return l.ToPlainText();
                case Heading h:
                    return $"**{h.ToPlainText()}**";
                default:
                    break;
            }

            return "";
        }
    }
}
