using System;
using System.Collections.Generic;
using System.Text;

using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using Discord;
/**
 * Written by Nathan C. Bishop
 */
namespace LiftDiscord.DiscordClient.Commands
{
    public class GenericCommandsModule : ModuleBase
    {
       
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

        [Command("Crafting")]
        public async Task CraftingGuide()
        {
            //Creates the URL to Ghazzy's video on crafting basics
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Delve Information thread")
                  .WithThumbnailUrl("https://i.imgur.com/CtVbxZC.png")
                  .WithUrl("https://youtu.be/ehngnPAn4NY")
                  .WithDescription("Ghazzy's basic crafting guide for new players that does a wonderful job explaining recipeas and orbs")
                  .WithFooter("https://youtu.be/ehngnPAn4NY")
                  .Build();

            await ReplyAsync(
           "Here is a basic crafting overview provided by https://www.twitch.tv/ghazzytv he covers everything you need to know about" +
           " crafting recipes, the different types of orbs, and what is the difference between a prefix and a suffix. ", false, embed, null);
        }

    }
}