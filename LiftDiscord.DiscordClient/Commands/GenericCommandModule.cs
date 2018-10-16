using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;
/**
* Written by Nathan C. Bishop
*/
namespace LiftDiscord.DiscordClient.Commands
{
    public class GenericCommandsModule : ModuleBase
    {
        //Guides Command
        [Command("guides")]
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
            "Engineering eternity has some wonderful build guides for new players. Here is a link to the playlist" +
            " so that you can get a better understanding of the game as a whole!", false, embed, null);
        }

        [Command("delve")]
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

        [Command("crafting")]
        public async Task CraftingGuide()
        {
            //Creates the URL to Ghazzy's video on crafting basics
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Intro to crafting")
                  .WithThumbnailUrl("https://i.imgur.com/CtVbxZC.png")
                  .WithUrl("https://youtu.be/ehngnPAn4NY")
                  .WithDescription("Ghazzy's basic crafting guide for new players that does a wonderful job explaining recipes and orbs")
                  .WithFooter("https://youtu.be/ehngnPAn4NY")
                  .Build();

            await ReplyAsync(
           "Here is a basic crafting overview provided by https://www.twitch.tv/ghazzytv he covers everything you need to know about" +
           " crafting recipes, the different types of orbs, and what is the difference between a prefix and a suffix. ", false, embed, null);
        }
        [Command("pobguide")]
        public async Task EngineeringEternityPoBGuide()
        {
            //Creates the URL variable that links to  build, needs to be updated if builds move
            var embed = new EmbedBuilder()
                  .WithColor(Color.Blue)
                  .WithTitle("Engineering Eternity's extensive guide on Path of Building")
                  .WithThumbnailUrl("https://i.imgur.com/SOB4UDM.jpg")
                  .WithUrl("https://www.youtube.com/watch?v=iLpKZVNTOo8")
                  .WithDescription("Engineering Eternity goes over everything you need to know when you beginning Path of Exile!")
                  .WithFooter("https://www.youtube.com/watch?v=iLpKZVNTOo8")
                  .Build();

            //Reply from the bot with the embeded URL as an argument
            await ReplyAsync(
            "Engineering eternity extensively covers Path of Building in the attached video that will give you an in depth look" +
            "at this essential Path of Exile utility", false, embed, null);
        }
    }

    [Group("pobhelp")]
    public class PobHelp : ModuleBase
    {
        [Command()]
        [Priority(1)]
        public async Task Index(params string[] args)
        {
            var embed = new EmbedBuilder()
                .AddField("PoB Help Commands",
                "!pobhelp import account\n" +
                "!pobhelp import pastebin\n" +
                "!pobhelp export\n" +
                "!pobguide").Build();

            await ReplyAsync("", embed: embed);
        }

        [Priority(2)]
        [Command("import pastebin")]
        public async Task ImportPastebinAsync()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://i.imgur.com/pHrlPko.png")
                .Build();

            await ReplyAsync("Follow these steps to import a PoB character from pastebin.", false, embed);
        }

        [Priority(2)]
        [Command("import account")]
        public async Task ImportAccountAsync()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://i.imgur.com/CCa1syC.png")
                .Build();

            await ReplyAsync("Follow these steps to import a PoB character from an account.", false, embed);
        }

        [Priority(2)]
        [Command("export")]
        public async Task ExportAsync()
        {
            var embed = new EmbedBuilder()
                .WithImageUrl("https://i.imgur.com/OMqP0Gi.png")
                .Build();

            await ReplyAsync("Follow these steps to export a PoB character to pastebin for sharing", false, embed);
        }
    }
}