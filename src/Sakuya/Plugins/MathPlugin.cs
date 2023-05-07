using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;

namespace Sakuya.Plugins;

public sealed class MathPlugin : Plugin
{
    [Command]
    [RegexShortcut( /* lang=regex */ @"^([\+\-]?\d*)的二次方$", "$1")]
    public MessageContent Square(int num)
    {
        return $"{num} 的二次方为 {Math.Pow(num, 2)}。";
    }
}