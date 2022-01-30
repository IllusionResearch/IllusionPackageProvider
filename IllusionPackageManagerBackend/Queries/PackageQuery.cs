using IllusionPackageDatabase;

namespace IllusionPackageManagerBackend.Queries;

public sealed class PackageQuery
{
    public IQueryable<PackageModel> GetPackages([Service] DatabaseContext database) => database.Packages;
}
