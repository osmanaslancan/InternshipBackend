using System.Reflection;

namespace InternshipBackend.Core.Services;

public interface ITypeSourceProvider
{
    Assembly[] Assemblies { get; }
}

public class TypeSourceProvider(params Assembly[] assemblies) : ITypeSourceProvider
{
    public Assembly[] Assemblies { get; } = assemblies;
    
    public IEnumerable<Type> FindTypesInheritedFrom<T>()
    {
        return FindTypesInheritedFrom(typeof(T));
    }
    
    public IEnumerable<Type> FindTypesInheritedFrom(Type inheritedType)
    {
        return Assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => inheritedType.IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false });
    }
}