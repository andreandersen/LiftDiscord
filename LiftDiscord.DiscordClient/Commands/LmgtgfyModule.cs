using Discord.Commands;
using System.Threading.Tasks;
using System.Net;

namespace LiftDiscord.DiscordClient.Commands
{
    public class LmgtgfyModule : ModuleBase
    {
        [Command("google")]
        public async Task LetMeGoogleThatForYou(params string[] term)
        {
            if (term.Length == 0)
            {
                await ReplyAsync(":slight_smile:");
                return;
            }

            await ReplyAsync("http://lmgtfy.com/?q=" + WebUtility.UrlEncode(string.Join(' ', term)));            
        }
    }
}
