using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiftDiscord.DiscordClient.Commands
{
    public class PoeWikiModule : ModuleBase
    {
        [Command("poewiki")]
        public async Task PoeWiki(params string[] args)
        {
            var noPeople = args.Where(c => !c.StartsWith("<")).ToList();
            var query = Uri.EscapeDataString(string.Join(' ', noPeople));

            var people = string.Join(' ', args.Where(c => c.StartsWith("<@") && c.EndsWith(">")));

            var reply = "";

            if (noPeople.Count == 0)
            {
                reply += "**Check out the Path of Exile Wiki here:** <https://pathofexile.gamepedia.com>";
            }
            else
            {
                try
                {
                    using (var cli = new HttpClient())
                    {
                        var queryUri = $"https://pathofexile.gamepedia.com/api.php?action=opensearch&search={query}&suggest=true&redirects=resolve";
                        var resultString = await cli.GetStringAsync(queryUri).ConfigureAwait(false);
                        var arrResult = JsonConvert.DeserializeObject<JArray>(resultString);

                        reply += (string)((JArray)arrResult.Last).First;
                    }
                }
                catch (Exception)
                {
                    reply += $"<https://pathofexile.gamepedia.com/index.php?search={query}>";
                }

            }
            if (string.IsNullOrWhiteSpace(reply))
            {
                reply = $"<https://pathofexile.gamepedia.com/index.php?search={query}>";
            }

            if (!string.IsNullOrWhiteSpace(people))
            {
                reply = $"{people}: " + reply;
            }

            await ReplyAsync(reply);
        }
    }

}
