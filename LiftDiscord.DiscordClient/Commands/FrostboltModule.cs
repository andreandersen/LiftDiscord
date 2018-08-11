using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class FrostboltModule : ModuleBase
    {
        [Command("Frostbolt")]
        public async Task liftingFrostbolt()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Liftings Frostbolt Totem Hierophant 3.3 Guide")
                  .WithThumbnailUrl("https://i.imgur.com/RFbq7b2.png")
                  .WithUrl("https://www.pathofexile.com/forum/view-thread/1931084")
                  .WithDescription("Liftings Frostbolt Hierophant is here to serve up some nice chilly death on a budget")
                  .WithFooter("https://www.pathofexile.com/forum/view-thread/1931084")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Liftings Frostbolt totem build is still viable :lnbFist:" + "\n" +
               "Here is the link to the forum page that is " + "updated every new league!", false, embed, null);
        }
    }
}