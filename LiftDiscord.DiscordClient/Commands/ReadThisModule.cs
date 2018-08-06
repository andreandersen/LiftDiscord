using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace LiftDiscord.DiscordClient.Commands
{
    public class ReadThisModule : ModuleBase
    {
        [Command("readthis")]
        public async Task ReadThisFirst()
        {
            var embed1 = new EmbedBuilder()
                .WithTitle("PLEASE READ BEFORE POSTING")
                .WithColor(Color.DarkGrey)
                .WithDescription(@"
The #poe-help channel and the PoE Helpers are here to help people with questions they __otherwise can't find answers to elsewhere, or if they need help with their current build__.

To get the best build 'review' first let us know your concern / curiosity and then preferrably paste your **[Path of Building](https://github.com/Openarl/PathOfBuilding/releases) pastebin link** so we can import it into our own PoB. This way we can see the finer details and give you a better answer. If you don't use or know how to use PoB then simply post a link to your poe profile and tell us the character name of the build in question.

If you ask a broad question or ask for a build suggestion, then make sure to provide __enough information__ so that the helpers can give an appropiate answer.

--> Type __!readmore__ for useful resources <--")
                .Build();

            var embed2 = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle("NO PRICE CHECKS")
                .WithDescription("*PoE-Helpers does not entertain price checks nor questions that can easily be answered via a simple google search.*")
                .Build();

            var embed3 = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle("NO RMT LINKS / DISCUSSION")
                .WithDescription("*Do not post links to sites that sell in-game currency for real money (RMT / Real Money Trading). It is strictly forbidden and a bannable offense to buy or sell currency for real money*")
                .Build();


            await ReplyAsync("", false, embed1);
            await ReplyAsync("", false, embed2);
            await ReplyAsync("", false, embed3);
        }

        [Command("readmore")]
        public async Task ReadMore()
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.DarkGrey)
                .WithTitle("USEFUL RESOURCES")
                .WithDescription("Please check the following applicable resources. This help us not having to repeat what's already been written about the subject.")
                .AddField(
                    new EmbedFieldBuilder()
                    .WithName("Path of Building")
                    .WithValue(
                        @"Path of Building is a character planning tool which also help you import and export theorycrafted/planned builds, 
and allow you to tinker around with your build to see how items and modifiers affect your character.

* Please download Path of Building [here](https://github.com/Openarl/PathOfBuilding/releases).
* Watch [this video](https://www.youtube.com/watch?v=iLpKZVNTOo8) to learn how to use Path of Building.
"))
                .AddField(new EmbedFieldBuilder()
                    .WithName("Path of Exile Community Wiki")
                    .WithValue(@"Both new and experienced players visit the [Wiki](https://pathofexile.gamepedia.com) quite a bit. It probably has the information you need. It's advised to dive into the [Wiki](https://pathofexile.gamepedia.com) and search around for new players, as it likely has your question answered.
You can find a list of [all unique items in the game](https://pathofexile.gamepedia.com/Unique_item), how [modifiers work](https://pathofexile.gamepedia.com/Modifiers), [Stats](https://pathofexile.gamepedia.com/Stat), [Damage](https://pathofexile.gamepedia.com/Damage), [Armor](https://pathofexile.gamepedia.com/Armour), [Evasion](https://pathofexile.gamepedia.com/Evasion), [Energy Shield (ES)](https://pathofexile.gamepedia.com/Energy_shield) and much more."
                ))
                .AddField(new EmbedFieldBuilder()
                    .WithName("Path of Exile Community Build Guides")
                    .WithValue(@"* [Path of Exile's Forum](https://www.pathofexile.com/forum/) is probably the best place to find build guides.
* Lifting's Build guides can be [found here](https://goo.gl/cn66uH)."))
                .AddField(new EmbedFieldBuilder().WithName("Other Useful Links")
                .WithValue(@"* Path of Exile's [Subreddit](https://www.reddit.com/r/pathofexile/)
* Other useful [Path of Exile Tools](https://www.youtube.com/watch?v=0xYQ5nM4kYs)"))
                .Build();

            await ReplyAsync("", false, embed);
        }

    }
}
