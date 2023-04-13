using Flandre.Adapters.OneBot;
using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Sakuya;

public static class OneBotAdapterFactory
{
    public static void AddOneBot(this IAdapterCollection adapters, IConfiguration configuration)
    {
        var config = configuration.Get<OneBotAdapterConfig>();
        adapters.Add(new OneBotAdapter(config ?? new OneBotAdapterConfig()));
    }
}