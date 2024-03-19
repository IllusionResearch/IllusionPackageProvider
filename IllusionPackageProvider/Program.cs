using IllusionPackageCore;
using IllusionPackageProvider;
using System.Collections.Concurrent;
using System.Text.Json;

var packages = Enum.GetValues<GameToken>().ToDictionary(k => k, v => new ConcurrentDictionary<string, WebPackage>());

await Parallel.ForEachAsync(Generator.Packages(), (result, _) =>
{
    if (packages.TryGetValue(result.Game, out var value))
    {
        value.TryAdd($"{result.Repository.Owner}/{result.Repository.Name}/{result.Name}", result.Package);
    }

    return ValueTask.CompletedTask;
});

if (!Directory.Exists("Public")) Directory.CreateDirectory("Public");

var opts = new JsonSerializerOptions
{
    WriteIndented = true,
};

await File.WriteAllTextAsync(Path.Combine("Public", "packages.json"), JsonSerializer.Serialize(packages.OrderBy(e => e.Key), opts));
