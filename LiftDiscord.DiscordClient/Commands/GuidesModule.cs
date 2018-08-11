using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class GuidesModule : ModuleBase
    {
        [Command("Guides")]
        public async Task EngineeringEternityNewPlayerGuides()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Engineering Eternity's new player guide series")
                  .WithThumbnailUrl("https://i.imgur.com/SOB4UDM.jpg")
                  .WithUrl("https://www.youtube.com/playlist?list=PL7GqFLebBU8inlrcqTZFXtIwP362XrdNe")
                  .WithDescription("Engineering Eternity goes over everything you need to know when you beginning Path of Exile!")
                  .WithFooter("https://www.youtube.com/playlist?list=PL7GqFLebBU8inlrcqTZFXtIwP362XrdNe")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Liftings warchief totem build is still viable :lnbFist:" + "\n" +
               "Here is the link to the forum page that is " + "updated every new league!", false, embed, null);
        }
    }
}
