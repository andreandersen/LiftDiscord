using System.Threading.Tasks;
using LiftDiscord.DiscordClient;

namespace LiftDiscord.ConsoleHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new Client(".....");
            await client.RunAsync();
        }
    }
}
