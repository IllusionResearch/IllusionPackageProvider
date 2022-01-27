using Octokit;

namespace IllusionPackageProvider;

internal static class Utils
{
    internal static GitHubClient CreateClient()
    {
        var header = new ProductHeaderValue("IllusionPackageProvider");
        var client = new GitHubClient(header);
        
        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        if (token is not null) client.Credentials = new Credentials(token);

        return client;
    }
}
