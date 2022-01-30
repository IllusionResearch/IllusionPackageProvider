using IllusionPackageDatabase;
using IllusionPackageManagerBackend.InputTypes;
using IllusionPackageManagerBackend.Mutations;
using IllusionPackageManagerBackend.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<PackageQuery>()
    .AddMutationType<PackageMutation>()
    .AddType<VersionInputType>();

builder.Services
    .AddDbContext<DatabaseContext>();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.MapGraphQL();

await app.RunAsync();
