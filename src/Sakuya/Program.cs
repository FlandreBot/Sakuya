using System.Text.Json;
using Flandre.Framework;
using Microsoft.Extensions.Hosting;
using Sakuya;
using Sakuya.Plugins;

var builder = FlandreApp.CreateBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppDomain.CurrentDomain.BaseDirectory
});

// 添加适配器
// builder.Adapters.AddKonata();
builder.Adapters.AddOneBot(builder.Configuration.GetSection("Adapters:OneBot"));

// 添加插件
builder.Plugins.Add<CommonPlugin>();
builder.Plugins.Add<MathPlugin>();

var app = builder.Build();

// 添加内置中间件
app.UseCommandSession();
app.UseCommandParser();
app.UseCommandInvoker();

app.OnReady += (_, _) =>
{
    // 更新设备信息
    File.WriteAllText("konata-device.json",
        JsonSerializer.Serialize(
            KonataAdapterFactory.DeviceInfo,
            new JsonSerializerOptions { WriteIndented = true }));
};

app.Run();