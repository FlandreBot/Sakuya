using System.Text.Json;
using Konata.Core.Common;

namespace Sakuya;

public static class KonataConfigManager
{
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

    public static BotConfig GetConfig() => ReadOrCreateNew("konata-config.json", () => new BotConfig
    {
        EnableAudio = true,
        TryReconnect = true,
        HighwayChunkSize = 8192
    });

    public static BotDevice GetDevice() => ReadOrCreateNew("konata-device.json", BotDevice.Default);

    public static BotKeyStore GetKeyStore() => ReadOrCreateNew("konata-keystore.json", () =>
    {
        Console.WriteLine("For first running, please type your account and password.");

        Console.Write("Account: ");
        var account = Console.ReadLine()?.Trim();

        Console.Write("Password: ");
        var password = Console.ReadLine()?.Trim();

        return new BotKeyStore(account, password);
    });
}