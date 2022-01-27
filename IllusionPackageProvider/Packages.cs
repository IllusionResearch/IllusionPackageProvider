using System.Runtime.CompilerServices;
using System.Text.Json;

namespace IllusionPackageProvider;

internal static class Packages
{
    internal async static IAsyncEnumerable<LocalPackage> GetPackages([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var file = File.OpenRead(@"Config\packages.json");
        var packages = JsonSerializer.DeserializeAsyncEnumerable<LocalPackage>(file, cancellationToken: cancellationToken);

        await foreach (var package in packages)
        {
            if (package is null) throw new ApplicationException("Bad package");

            yield return package;
        }
    }
}
