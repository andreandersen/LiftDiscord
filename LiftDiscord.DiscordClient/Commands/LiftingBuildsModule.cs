using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class LiftingBuildsModule : ModuleBase
    {
        //Frostbolt command
        [Command("frostbolt")]
        public async Task liftingFrostbolt()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Liftings frostbolt Totem Hierophant 3.4 Guide")
                  .WithThumbnailUrl("https://i.imgur.com/RFbq7b2.png")
                  .WithUrl("https://www.pathofexile.com/forum/view-thread/1931084")
                  .WithDescription("Liftings Frostbolt Hierophant is here to serve up some nice chilly death on a budget")
                  .WithFooter("https://www.pathofexile.com/forum/view-thread/1931084")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Liftings frostbolt totem build is still viable! <:lnbFist:480141292417056768> " + "\n" +
               "Here is the link to the forum page that is " + "updated every new league!", false, embed, null);
        }

        //Warchief Command
        [Command("warchief")]
        public async Task LiftingWarchief()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Liftings Warchief Totem Champion 3.4 Guide")
                  .WithThumbnailUrl("https://i.imgur.com/RFbq7b2.png")
                  .WithUrl("https://www.pathofexile.com/forum/view-thread/1960538")
                  .WithDescription("Liftings Warchief totem is locked and loaded for some more brotastic murder")
                  .WithFooter("https://www.pathofexile.com/forum/view-thread/1960538")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Liftings warchief totem build is still viable! <:lnbFist:480141292417056768> " + "\n" +
               "Here is the link to the forum page that is " + "updated every new league!", false, embed, null);
        }
    }
}