using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Reflection;

namespace LiftDiscord.DiscordClient.Commands
{
    public class HelpModule : ModuleBase
    {
        private static IReadOnlyList<string> s_commands;

        [Command("help")]
        public async Task Help()
        {
            await ReplyAsync("Available commands:\n" + string.Join('\n', s_commands));
        }

        static HelpModule()
        {
            s_commands = typeof(Client).Assembly
                .DefinedTypes
                .Where(c => c.BaseType == typeof(ModuleBase))
                .SelectMany(e =>
                    e.DeclaredMethods.Where(m =>
                    m.CustomAttributes.Any(a =>
                    a.AttributeType == typeof(CommandAttribute))), (c, m) => GetCommand(c, m))
                .ToList();

            string GetCommand(TypeInfo t, MethodInfo m)
            {
                var builder = new StringBuilder("!");

                var group = t.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(GroupAttribute));

                if (group != null && group.ConstructorArguments.Count > 0)
                {
                    builder
                        .Append(group.ConstructorArguments
                            .FirstOrDefault().Value + " ");
                }

                var cmdName = m.CustomAttributes
                    .First(c => c.AttributeType == typeof(CommandAttribute))
                        .ConstructorArguments.FirstOrDefault().Value as string;

                if (!string.IsNullOrEmpty(cmdName))
                {
                    builder.Append(cmdName);
                    var paramseters = m.GetParameters().Select(c => c.Name).ToList();
                    foreach (var item in paramseters)
                    {
                        builder.Append(" " + item);
                    }
                }

                var cmd = builder.ToString();

                return cmd;
            }
        }

    }
}
