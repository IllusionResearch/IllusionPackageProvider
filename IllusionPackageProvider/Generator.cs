using IllusionPackageDatabase;

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
                var result = releases.TryCreateResult(package);
                if (result is not null) yield return result;
            }
        }
    }
}
