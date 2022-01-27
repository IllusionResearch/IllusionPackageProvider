using IllusionPackageCore;
using System.Runtime.InteropServices;

namespace IllusionPackageProvider.Attributes;

internal abstract class GameAttribute : Attribute
{
    internal GameToken Token { get; init; }
    internal Architecture Arch { get; init; }
}
