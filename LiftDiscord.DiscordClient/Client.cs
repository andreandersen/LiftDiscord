using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LiftDiscord.PathOfBuilding;
using Microsoft.Extensions.DependencyInjection;

namespace LiftDiscord.DiscordClient
{
    public class Client
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private string _token;

        public Client(string token)
        {
            _token = token;

            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
            };

            _client = new DiscordSocketClient(config);
#if DEBUG
            _client.Log += LogAsync;
#endif
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton(new PasteBinFetcher())
                .AddSingleton(new Parser())
                .BuildServiceProvider();
        }

        public async Task RunAsync()
        {
            await InstallCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync("Command Lifting");

            var user = _client.GetUser(323);

            await Task.Delay(-1);
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommandAsync;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(typeof(Client).Assembly);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            await ScanForNoNoLinks(message);
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            // Create a Command Context
            var context = new SocketCommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess && result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        static readonly string[] _godRoles = new[] { "admin", "community manager", "moderators", "poe helpers", "el bot" };
        static readonly string[] _disallowedSites = new[]
        {
            "notcool.com",
            "duducool.com",
            "epicnpc.com",
            "g2g.com",
            "gaming4ez.com",
            "gm2v.com",
            "gramno.com",
            "igmmo.com",
            "igvault.",
            "items7.com",
            "mmobeys.com",
            "mmocs.com",
            "mmogah.com",
            "mmogo.com",
            "mulefactory.com",
            "odealo.com",
            "ouo.com",
            "ouo.io",
            "ouo.press",
            "ownedcore.com",
            "playerauctions.com",
            "poe4orbs.com",
            "poebuilds.net",
            "poe-market.com",
            "poecurrency.shop",
            "poecurrencybuy.com",
            "r4pg.com",
            "u4gm.com",
            "raiditem.com",
            "seagm.com",
            "sellersandfriends.com",
            "taazatadka.com",
        };

        private async Task ScanForNoNoLinks(SocketUserMessage msg)
        {
            var godRole = (msg.Author as SocketGuildUser).Roles.Select(c => c.Name.ToLower()).Any(c => _godRoles.Contains(c));
            if (godRole) return;

            var msgLower = msg.Content.ToLower();
            var anyDisallowed = _disallowedSites.Any(c => msgLower.Contains(c));

            if (anyDisallowed)
            {
                await msg.DeleteAsync();

                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName("Brotherhood Moderation"),
                    Color = Color.Red,
                    Title = "Warning / Auto-Moderation",
                    Description =
                        $"{msg.Author.Mention}, the domain you used or the page your linked is associated with an RMT website, or another type of disallowed websites.\r\n\r\n" +
                        $"_Your link has been automatically been logged to the moderator team_."
                }.Build();

                await msg.Channel.SendMessageAsync($"", false, embed);

                var log = (msg.Channel as SocketTextChannel).Guild.Channels.FirstOrDefault(c => c.Name == "moderators-log") as SocketTextChannel;

                var reportEmbed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder
                    {
                        Name = msg.Author.Username + "#" + msg.Author.DiscriminatorValue,
                        IconUrl = msg.Author.GetAvatarUrl()
                    },
                    Description = msg.Content
                }.Build();

                await log.SendMessageAsync($"The following message from {msg.Author.Mention} was automatically removed", false, reportEmbed);

            }
        }

        private async Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
        }
    }
}
