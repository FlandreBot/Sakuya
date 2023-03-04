using Flandre.Adapters.OneBot;
using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Sakuya;

public static class OneBotAdapterFactory
{
    public static void AddOneBotAdapter(this FlandreAppBuilder builder, IConfiguration configuration)
    {
        var config = configuration.Get<OneBotAdapterConfig>();
        builder.AddAdapter(new OneBotAdapter(config ?? new OneBotAdapterConfig()));
    }
}