using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace LiftDiscord.DiscordClient.Commands
{
    [Group("subworld")]
    public class SubworldModule : ModuleBase
    {
        [Command()]
        [Priority(1)]
        public async Task Index(params string[] args)
        {
            var embed = new EmbedBuilder()

                .AddField("Subworld Commands",
                "**I want to be added to Subworld**\n!subworld add <twitch username> <country, city>" +
                "\n_Example: !subworld add User123 Chicago, United States_" +
                "\n\n**I want to be removed**\n!subworld remove <twich username>")
                .WithThumbnailUrl("https://i.imgur.com/uRoC62S.png")
                .Build();

            await ReplyAsync("", false, embed);
        }



        [Command("add")]
        [Priority(10)]
        public async Task Add(string twitchUsername, [Remainder] string location)
        {
            var msg = Context.Message as SocketUserMessage;
            var user = Context.User as IGuildUser;

            if (msg == null || user == null)
                return;

            if (string.IsNullOrWhiteSpace(location) || twitchUsername.Length < 2)
            {
                await ReplyAsync("Please provide a twitch username and location if you want to be added to Subworld.\n" +
                                "Like this: !subworld add User123 Chicago, United States");
                return;
            }

            var reportEmbed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = msg.Author.ToString() + " " + user.Nickname,
                    IconUrl = msg.Author.GetAvatarUrl()
                }
            }
            .WithDescription("Add to Subworld")
            .AddField("Twitch Username", twitchUsername, true)
            .AddField("Location", location, true)
            .WithColor(Color.Green)
            .Build();

            var log = (msg.Channel as SocketTextChannel).Guild.Channels.FirstOrDefault(c => c.Name == "subworld-log") as SocketTextChannel;
            await log.SendMessageAsync("", false, reportEmbed);

            await msg.AddReactionAsync(new Emoji("✅"));

            _ = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                await msg.DeleteAsync();
            });
        }

        [Command("remove")]
        [Priority(9)]
        [Alias("delete", "del")]
        public async Task Remove(string twitchUsername)
        {
            var msg = Context.Message as SocketUserMessage;
            var user = Context.User as IGuildUser;

            if (msg == null || user == null)
                return;

            if (twitchUsername.Length < 2)
                return;

            var reportEmbed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = msg.Author.ToString() + " " + user.Nickname,
                    IconUrl = msg.Author.GetAvatarUrl()
                }
            }
            .WithDescription("Remove from Subworld")
            .AddField("Twitch Username", twitchUsername, false)
            .WithColor(Color.Red)
            .Build();

            var log = (msg.Channel as SocketTextChannel).Guild.Channels.FirstOrDefault(c => c.Name == "subworld-log") as SocketTextChannel;
            await log.SendMessageAsync("", false, reportEmbed);

            await msg.AddReactionAsync(new Emoji("✅"));

            _ = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                await msg.DeleteAsync();
            });
        }

    }
}
