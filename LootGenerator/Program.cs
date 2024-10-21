using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LootGenerator.Interface;
using LootGenerator.Handler;
using LootGenerator.Service;

namespace LootGenerator;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IDiceService, DiceService>();
                services.AddSingleton<IGoldService, GoldService>();
                services.AddSingleton<IGemstoneService, GemstoneService>();
                services.AddSingleton<ILootHandler, LootHandler>();
                services.AddSingleton<IMenuHandler, MenuHandler>();
                services.AddHostedService<GraphicUserInterface>();
            });
        var host = builder.Build();
        await host.RunAsync();

        await host.StopAsync();

        Environment.Exit(0);
    }
}