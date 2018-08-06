using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using LiftDiscord.PathOfBuilding;
using LiftDiscord.PathOfBuilding.Models;

namespace LiftDiscord.DiscordClient.Commands
{
    public class PobModule : ModuleBase
    {
        Parser _parser;
        PasteBinFetcher _pastebinFetcher;

        public PobModule(Parser parser, PasteBinFetcher pastebinFetcher)
        {
            _parser = parser;
            _pastebinFetcher = pastebinFetcher;
        }

        [Command("pob", RunMode = RunMode.Async)]
        public async Task Pob(string url = "")
        {
            try
            {
                var base64 = await _pastebinFetcher.GetRawCode(url);
                var character = _parser.ParseCode(base64);
                var e =
                    GenerateStats(character)
                    .WithAuthor(Context.Message.Author)
                    .WithFooter($"<< !pob {url}")
                    .Build();

                await ReplyAsync("", false, e);
            }
            catch (Exception ex)
            {
                await ReplyAsync("Error fetching build data.");
            }
        }

        private EmbedBuilder GenerateStats(Character c)
        {           
            var s = c.Summary;

            var defenses = "```" +
                StatLine("Life", s.Life) +
                (s.Life != s.LifeUnreserved ? StatLine("Life Unres", s.LifeUnreserved) : "") +
                StatLine("Net Life Reg", s.NetLifeRegen) +
                StatLine("Mana", s.Mana) +
                (s.Mana != s.ManaUnreserved ? StatLine("Mana Unres", s.ManaUnreserved) : "") +
                StatLine("ES", s.EnergyShield) +
                StatLine("Armour", s.Armour) +
                StatLine("Evasion", s.Evasion) + "\r\n" +
                StatLine("Fire Res", s.FireResist, suffix: "%") +
                StatLine("Cold Res", s.ColdResist, suffix: "%") +
                StatLine("Lightn. Res", s.LightningResist, suffix: "%") +
                StatLine("Chaos Res", s.ChaosResist, suffix: "%") +
                "```";

            var mainSkill = c.Skills.MainSkillGroup;
            var skills =
                string.Join(", ", mainSkill.Gems.Where(e => e.Enabled).Select(e => e.Name));

            var offenses = "```" +
                StatLine("APS", s.Speed, precision: 2) +
                StatLine("Avg Damage", s.AverageDamage) +
                StatLine("Tot. DPS", s.TotalDPS) +
                StatLine("Tot. DoT", s.TotalDot) +
                (s.WithPoisonDPS != s.TotalDPS ? StatLine("With Poison", s.WithPoisonDPS, colWidth: 14) : "") +
                StatLine("Crit Chance", s.CritChance, suffix: "%") +
                StatLine("Crit Mult", s.CritMultiplier, precision: 2) +
                StatLine("Bleed DPS", s.BleedDPS) +
                StatLine("Ignite DPS", s.IgniteDPS) + "\r\n" +
                skills + "```";

            return new EmbedBuilder()
                .WithColor(Color.Blue)
                .WithTitle($"{c.Class} ({c.Ascendancy}), level {c.Level}")
                .WithDescription($"```STR: {s.Str}, DEX: {s.Dex}, INT: {s.Int}```")
                .AddField("Defense", defenses, true)
                .AddField("Offense", offenses, true);
        }

        private static string StatLine(string col, float val, bool ignoreIfZero = true, bool appendLineBreak = true, 
            int colWidth = 14, int valWidth = 9, int precision = 0, string suffix = "")
        {
            if (val == 0 && ignoreIfZero) return "";

            var rVal = Math.Round(val, precision);
            var format = "###,###,###0";
            if (precision > 0)
                format += $".{new string('0', precision)}";

            var strval = (rVal.ToString(format) + suffix).PadLeft(valWidth);
            if (appendLineBreak)
                strval += "\r\n";

            return col.PadRight(colWidth) + strval;
        }
    }
}
