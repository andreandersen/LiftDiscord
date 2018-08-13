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

        //Guides Command
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

        //Warchief Command
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
               "Here is the link to the forum page that is " + "updated every new league!", false, embed, null);
        }

        [Command("Delve")]
        public async Task DelveLeague()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Delve Information thread")
                  .WithThumbnailUrl("https://i.imgur.com/RFbq7b2.png")
                  .WithUrl("https://old.reddit.com/r/pathofexile/comments/962kqi/patch_34_delve_league_compiled_information_thread/")
                  .WithDescription("A compiled reddit thread with delve information")
                  .WithFooter("https://old.reddit.com/r/pathofexile/comments/962kqi/patch_34_delve_league_compiled_information_thread/")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Delve league is planned to start on 08/31/2018, you can check the path of exile website to see the exact time it launches." +
            " Below is a link to a compilation of information about patch 3.4 and delve league itself. ", false, embed, null);
        }
    }
}