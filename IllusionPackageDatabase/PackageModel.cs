using IllusionPackageCore;

namespace IllusionPackageDatabase;

public sealed class PackageModel
{
    public int Id { get; set; }
    public GameToken Game { get; set; }
    public Version Version { get; set; } = new Version();
    public string Pattern { get; set; } = string.Empty;
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
}
