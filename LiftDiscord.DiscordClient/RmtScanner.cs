using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace LiftDiscord.DiscordClient
{
    public class RmtScanner
    {
        private static readonly string[] _godRoles = new[] { "admin", "community manager", "moderators", "poe helpers", "el bot" };

        private static readonly string[] _disallowedSites = new[]
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
            "poecurrency.gg",
            "poecurrency.shop",
            "poecurrencybuy.com",
            "r4pg.com",
            "u4gm.com",
            "raiditem.com",
            "seagm.com",
            "sellersandfriends.com",
            "taazatadka.com",
        };

        public RmtScanner()
        {
        }

        internal static async Task ScanForNoNoLinks(SocketUserMessage msg)
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
    }
}