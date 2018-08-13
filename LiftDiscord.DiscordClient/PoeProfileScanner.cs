using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace LiftDiscord.DiscordClient
{
    public static class PoeProfileScanner
    {
        private static Regex matcher =
            new Regex(@"http[s]\:\/\/[w][w][w][.]pathofexile\.com\/account\/view.profile\/([\w\d_-]{0,})",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        internal static async Task ScanForProfileUrls(SocketUserMessage msg)
        {
            var match = matcher.Match(msg.Content);
            if (match.Success)
            {
                var accountName = match.Groups[1].Value;
                using (var cli = new HttpClient())
                {
                    var apiUrl =
                        $"https://www.pathofexile.com/character-window/get-characters?accountName={accountName}";

                    var ret = await cli.GetAsync(apiUrl);
                    if (ret.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        await msg.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            .WithTitle($"Account {accountName} could not be found.")
                            .WithDescription(
                                "Your account is likely marked as private, and to make it possible for others to view your characters" +
                                $", please visit the following link: https://www.pathofexile.com/account/view-profile/{accountName}/privacy" +
                                " and untick 'Hide Characters tab' and 'Set profile as private' and then press Update")
                            .WithThumbnailUrl("https://i.imgur.com/qRcmaHJ.png")
                            .Build());
                    }
                }
            }
        }
    }
}
