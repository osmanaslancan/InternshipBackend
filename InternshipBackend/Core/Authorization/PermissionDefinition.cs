namespace InternshipBackend.Core.Authorization;

public class PermissionDefinition(string name)
{
    public string Name { get; set; } = name;
    public ICollection<PermissionDefinition> Children { get; set; } = [];
}