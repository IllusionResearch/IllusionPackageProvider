using IllusionPackageCore;
using IllusionPackageDatabase;
using System.Text.RegularExpressions;

namespace IllusionPackageProvider;

internal static class Generator
{
    internal static async IAsyncEnumerable<GameResult> Packages()
    {
        var client = Utils.CreateClient();

        await using var database = new DatabaseContext();

        foreach (var packages in database.Packages.ToArray().GroupBy(k => new { k.RepositoryOwner, k.RepositoryName }))
        {
            var releases = await client.Repository.Release.GetAll(packages.Key.RepositoryOwner, packages.Key.RepositoryName);

            foreach (var package in packages)
            {
                foreach (var release in releases)
                {
                    foreach (var asset in release.Assets)
                    {
                        var math = Regex.Match(asset.Name, package.Pattern);
                        if (!math.Success) continue;

                        var name = math.Groups.GetValueOrDefault("Name");
                        if (name is null) throw new ApplicationException($"No name found. Bad pattern ({package.Pattern})");

                        var version = math.Groups.GetValueOrDefault("Version")?.Value ?? release.TagName;

                        var repository = new Repository(packages.Key.RepositoryOwner, packages.Key.RepositoryName);
                        yield return new GameResult(package.Game, repository, name.Value, new WebPackage(asset.BrowserDownloadUrl, Version.Parse(version)));
                    }
                }
            }
        }
    }
}
