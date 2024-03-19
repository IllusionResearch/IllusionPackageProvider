using IllusionPackageCore;
using IllusionPackageDatabase;
using Octokit;
using System.Text.RegularExpressions;

namespace IllusionPackageProvider;

internal static class ReleaseExtension
{
    internal static GameResult? TryCreateResult(this IReadOnlyList<Release> releases, PackageModel model)
    {
        foreach (var release in releases)
        {
            foreach (var asset in release.Assets)
            {
                var math = Regex.Match(asset.Name, model.Pattern);
                if (!math.Success) continue;
                
                var name = math.Groups.GetValueOrDefault("Name");
                if (name is null) throw new ApplicationException($"No name found. Bad pattern ({model.Pattern})");

                var version = math.Groups.GetValueOrDefault("Version")?.Value ?? release.TagName;

                var repository = new Repository(model.RepositoryOwner, model.RepositoryName);
                return new GameResult(model.Game, repository, name.Value, new WebPackage(asset.BrowserDownloadUrl, Version.Parse(version)));
            }
        }

        return null;
    }
}
