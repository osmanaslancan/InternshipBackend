namespace InternshipBackend.Core;

public abstract class Entity : IEntity, IHasIdField
{
    public int Id { get; set; }
}
