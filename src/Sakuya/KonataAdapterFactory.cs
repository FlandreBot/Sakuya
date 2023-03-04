using System.Text.Json;
using Flandre.Adapters.Konata;
using Flandre.Framework;
using Konata.Core.Common;

namespace Sakuya;

public static class KonataAdapterFactory
{
    public static BotConfig KonataConfig { get; } = GetConfig();
    public static BotDevice DeviceInfo { get; } = GetDevice();
    public static BotKeyStore KeyStore { get; } = GetKeyStore();

    public static void AddKonataAdapter(this FlandreAppBuilder builder)
    {
        var config = new KonataAdapterConfig();
        config.Bots.Add(new KonataBotConfig
        {
            Konata = KonataConfig,
            Device = DeviceInfo,
            KeyStore = KeyStore
        });
        builder.AddAdapter(new KonataAdapter(config));
    }

    private static T ReadOrCreateNew<T>(string file, Func<T> createNew)
    {
        if (File.Exists(file))
            return JsonSerializer.Deserialize<T>(File.ReadAllText(file))!;

        var obj = createNew();
        var deviceJson = JsonSerializer.Serialize(obj,
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(file, deviceJson);
        return obj;
    }

    private static BotConfig GetConfig() => ReadOrCreateNew("konata-config.json", () => new BotConfig
    {
        EnableAudio = true,
        TryReconnect = true,
        HighwayChunkSize = 8192
    });

    private static BotDevice GetDevice() => ReadOrCreateNew("konata-device.json", BotDevice.Default);

    private static BotKeyStore GetKeyStore() => ReadOrCreateNew("konata-keystore.json", () =>
    {
        Console.WriteLine("For first running, please type your account and password.");

        Console.Write("Account: ");
        var account = Console.ReadLine()?.Trim();

        Console.Write("Password: ");
        var password = Console.ReadLine()?.Trim();

        return new BotKeyStore(account, password);
    });
}