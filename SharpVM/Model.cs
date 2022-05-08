public class _Object
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public enum Type
{
    ClusterComputeResource = 1,
    VirtualMachine,
    Datastore,
    PortGroup
}

public class _GuesOs
{
    public string Name { get; set; }
    public string Value { get; set; }
}
