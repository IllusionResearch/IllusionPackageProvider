using IllusionPackageCore;
using System.Text.RegularExpressions;

namespace IllusionPackageProvider;

internal static class Generator
{
    internal static IAsyncEnumerable<IEnumerable<GameResult>> Packages()
    {
        var client = Utils.CreateClient();
        
        return IllusionPackageProvider.Packages.GetPackages().SelectAwait(async package =>
        {
            var releases = await client.Repository.Release.GetAll(package.Repository.Owner, package.Repository.Name);

            return package.Games.Select(game =>
            {
                var packages = game.Patterns.Select(pattern =>
                {
                    foreach (var release in releases)
                    {
                        foreach (var asset in release.Assets)
                        {
                            var math = Regex.Match(asset.Name, pattern);
                            if (!math.Success) continue;

                            var version = math.Groups.Count >= 2 ? math.Groups[1].Value : release.TagName;
                            return new WebPackage(asset.BrowserDownloadUrl, Version.Parse(version));
                        }
                    }


                    throw new ApplicationException($"Pattern \"{pattern}\" not found");
                });

                return new GameResult(game.Token, package.Repository, packages);
            });
        });
    }
}
