using Flandre.Core.Messaging;
using Flandre.Framework.Common;

namespace Sakuya.Plugins;

public sealed class CommonPlugin : Plugin
{
    public override async Task OnMessageReceivedAsync(MessageContext ctx)
    {
        if (ctx.Message.GetText() == "Hello!")
            await ctx.Bot.SendMessageAsync(ctx.Message, "World!");
    }
}