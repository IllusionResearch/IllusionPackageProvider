using IllusionPackageCore;

namespace IllusionPackageProvider;

internal record GameResult(GameToken Token, Repository Repository, IEnumerable<WebPackage> Packages);
