using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class WarchiefModule : ModuleBase
    {
        [Command("Warchief")]
        public async Task LiftingWarchief()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Liftings Warchief Totem Champion 3.3 Guide")
                  .WithThumbnailUrl("https://i.imgur.com/RFbq7b2.png")
                  .WithUrl("https://www.pathofexile.com/forum/view-thread/1960538")
                  .WithDescription("Liftings Warchief totem is locked and loaded for some more brotastic murder")
                  .WithFooter("https://www.pathofexile.com/forum/view-thread/1960538")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Liftings warchief totem build is still viable :lnbFist:" + "\n" +
               "Here is the link to the forum page that is " + "updated every new league!", false,embed,null);
        }
    }
}
