using IllusionPackageCore;

namespace IllusionPackageManagerBackend.Payload;

public sealed record AddPackagePayload(GameToken Game, string Pattern, string RepositoryOwner, string RepositoryName);
