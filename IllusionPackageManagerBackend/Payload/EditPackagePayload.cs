using IllusionPackageCore;

namespace IllusionPackageManagerBackend.Payload;

public sealed record EditPackagePayload(int Id, GameToken Game, string Pattern, string RepositoryOwner, string RepositoryName);
