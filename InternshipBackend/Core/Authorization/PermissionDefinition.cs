namespace InternshipBackend.Core.Authorization;

public class PermissionDefinition(string name, PermissionRequirementType type)
{
    public string Name { get; set; } = name;
    public PermissionRequirementType Type { get; set; } = type;
    public ICollection<PermissionDefinition> Children { get; set; } = [];
}