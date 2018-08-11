using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Linq;
using LiftDiscord.DiscordClient.Utils.PoeWiki;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class WhyDoIDieModule : ModuleBase
    {
        [Command("WhyDoIDie")]
        public async Task WhyDoIDie(params string[] args)
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var people = string.Join(' ', args.Where(c => c.StartsWith("<@") && c.EndsWith(">")));

        }
    }
}
