using IllusionPackageDatabase;
using IllusionPackageManagerBackend.Payload;
using Microsoft.EntityFrameworkCore;

namespace IllusionPackageManagerBackend.Mutations;

public sealed class PackageMutation
{
    public async ValueTask<PackageModel> AddPackage(AddPackagePayload payload, [Service] DatabaseContext database, CancellationToken cancellationToken)
    {
        var model = await database.Packages.AddAsync(new PackageModel
        {
            Game = payload.Game,
            Pattern = payload.Pattern,
            RepositoryOwner = payload.RepositoryOwner,
            RepositoryName = payload.RepositoryName,
        }, cancellationToken);
        
        await database.SaveChangesAsync(cancellationToken);

        return model.Entity;
    }

    public async ValueTask<PackageModel> EditPackage(EditPackagePayload payload, [Service] DatabaseContext database, CancellationToken cancellationToken)
    {
        var model = await database.Packages.FirstAsync(p => p.Id == payload.Id, cancellationToken);

        model.Game = payload.Game;
        model.Pattern = payload.Pattern;
        model.RepositoryOwner = payload.RepositoryOwner;
        model.RepositoryName = payload.RepositoryName;

        await database.SaveChangesAsync(cancellationToken);

        return model;
    }
}
