using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Linq;
using LiftDiscord.DiscordClient.Utils.PoeWiki;
using Discord;

namespace LiftDiscord.DiscordClient.Commands
{
    public class PoeWikiModule : ModuleBase
    {
        const string thumbnailUrl = "https://i.imgur.com/p1xdSCW.png";

        [Command("poewiki")]
        public Task PoeWikiAsync(params string[] term) => WikiAsync(term);

        [Command("wiki")]
        public async Task WikiAsync(params string[] term)
        {
            var noPeople = term.Where(c => !c.StartsWith("<")).ToList();

            var query = Uri.EscapeDataString(string.Join(' ', noPeople));
            var hasQuery = !string.IsNullOrEmpty(query);

            var people = string.Join(' ', term.Where(c => c.StartsWith("<@") && c.EndsWith(">")));

            if (!hasQuery)
            {
                await ReplyWithWikiIndexAsync(people);
                return;
            }

            var (title, pageResultFromQuery) = await WikiFetcher.OpenSearchAsync(query);
            var hasPageResult = !string.IsNullOrEmpty(pageResultFromQuery);

            if (!hasPageResult)
            {
                await ReplyWithWikiManualSearch(people, query);
                return;
            }

            var pageText = await WikiFetcher.GetWikiPage(title);
            await ReplyWithWikiPageAndText(people, pageResultFromQuery, title, pageText);
        }

        private Task ReplyWithWikiIndexAsync(string peopleTags)
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.Blue)
                .WithTitle("Path of Exile Wiki")
                .WithThumbnailUrl(thumbnailUrl)
                .WithUrl("https://pathofexile.gamepedia.com")
                .WithDescription("Go [here](https://pathofexile.gamepedia.com) to read more about Path of Exile. There's a lot to learn. Oh, there's a search feature as well.")
                .WithFooter("https://pathofexile.gamepedia.com")
                .Build();

            return ReplyAsync(peopleTags, false, embed);
        }

        private Task ReplyWithWikiManualSearch(string peopleTags, string query)
        {
            var decodedQuery = Uri.UnescapeDataString(query);
            var embed = new EmbedBuilder()
                .WithColor(Color.Blue)
                .WithTitle($"Path of Exile Wiki: Search for {decodedQuery}")
                .WithThumbnailUrl(thumbnailUrl)
                .WithUrl($"https://pathofexile.gamepedia.com/index.php?search={query}")
                .WithDescription($"[Search the wiki for {decodedQuery}](https://pathofexile.gamepedia.com/index.php?search={query}), and you shall find your answer.")
                .WithFooter($"https://pathofexile.gamepedia.com")
                .Build();

            return ReplyAsync(peopleTags, false, embed);
        }

        private Task ReplyWithWikiPageAndText(string people, string url, string title, string text)
        {
            text = text + $"\n\n**[Read more here...]({url})**";

            var embed = new EmbedBuilder()
                .WithColor(Color.Green)
                .WithTitle($"Path of Exike Wiki: {title}")
                .WithThumbnailUrl(thumbnailUrl)
                .WithDescription(text)
                .WithUrl($"{url}")
                .WithFooter(url)
                .Build();

            return ReplyAsync(people, false, embed);
        }
    }

}
