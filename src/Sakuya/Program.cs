using System.Text.Json;
using Flandre.Adapters.Konata;
using Flandre.Framework;
using Microsoft.Extensions.Hosting;
using Sakuya;
using Sakuya.Plugins;

var builder = FlandreApp.CreateBuilder(args);

// 添加 Konata 适配器
var kntDevice = KonataConfigManager.GetDevice();
var kntAdapterConfig = new KonataAdapterConfig();
kntAdapterConfig.Bots.Add(new KonataBotConfig
{
    Konata = KonataConfigManager.GetConfig(),
    Device = kntDevice,
    KeyStore = KonataConfigManager.GetKeyStore()
});
builder.AddAdapter(new KonataAdapter(kntAdapterConfig));

// 添加插件
builder.AddPlugin<CommonPlugin>();
builder.AddPlugin<MathPlugin>();

var app = builder.Build();

// 添加内置中间件
app.UseCommandSession();
app.UseCommandParser();
app.UseCommandInvoker();

app.OnReady += (_, _) =>
{
    // 更新设备信息
    File.WriteAllText("konata-device.json",
        JsonSerializer.Serialize(kntDevice, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
};

app.Run();