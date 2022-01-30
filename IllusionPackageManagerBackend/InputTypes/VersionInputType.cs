namespace IllusionPackageManagerBackend.InputTypes;

public sealed class VersionInputType : InputObjectType<Version>
{
    protected override void Configure(IInputObjectTypeDescriptor<Version> descriptor)
    {
        descriptor.Ignore(t => t.MajorRevision);
        descriptor.Ignore(t => t.MinorRevision);
    }
}