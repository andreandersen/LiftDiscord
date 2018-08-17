using System.Threading.Tasks;
using LiftDiscord.DiscordClient;

namespace LiftDiscord.ConsoleHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new Client("NDExNTIzNDcxODQ4Mzc0Mjcy.DkigUw.0p-Z1ym3keA47jD8ePx-GxwJaj4");
            await client.RunAsync();
        }
    }
}
