using IllusionPackageCore;
using System.Runtime.InteropServices;

namespace IllusionPackageProvider.Attributes.Games;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal sealed class KoikatsuGame : GameAttribute
{
    public KoikatsuGame()
    {
        Token = GameToken.Koikatsu;
        Arch = Architecture.X64;
    }
}
