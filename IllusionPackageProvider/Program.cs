using IllusionPackageCore;
using IllusionPackageProvider;
using System.Collections.Concurrent;
using System.Text.Json;

var packages = Enum.GetValues<GameToken>().ToDictionary(k => k, v => new ConcurrentDictionary<string, WebPackage>());

await Parallel.ForEachAsync(Generator.Packages(), (package, _) =>
{
    foreach (var result in package)
    {
        if (packages.TryGetValue(result.Token, out var value))
        {
            foreach (var item in result.Packages)
            {
                value.TryAdd($"{result.Repository.Owner}/{result.Repository.Name}", item);
            }
        }
    }

    return ValueTask.CompletedTask;
});

if (!Directory.Exists("Public")) Directory.CreateDirectory("Public");

await File.WriteAllTextAsync("Public/packages.json", JsonSerializer.Serialize(packages));
