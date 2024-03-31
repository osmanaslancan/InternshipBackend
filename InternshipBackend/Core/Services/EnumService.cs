using System.ComponentModel;
using System.Reflection;
using InternshipBackend.Resources.Enum;
using Microsoft.Extensions.Localization;

namespace InternshipBackend.Core.Services;

public interface IEnumService
{
    List<EnumDto>? GetEnumOrDefault(string key);
}

public class EnumService(IStringLocalizer<Enums> stringLocalizer, ITypeSourceProvider typeSourceProvider) : IEnumService, ISingletonService
{
    public List<EnumDto>? GetEnumOrDefault(string key)
    {
        var type = FindEnumType(key);

        if (type is null)
        {
            return null;
        }

        var result = new List<EnumDto>();

        foreach (var item in Enum.GetValues(type))
        {
            var candidate = stringLocalizer[$"{item.GetType().Name}.{item}"];

            result.Add(new EnumDto
            {
                Id = item.ToString(),
                Name = GetDescription((Enum)item) ?? (!candidate.ResourceNotFound ? candidate : item.ToString())
            });
        }
        return result;
    }

    private string? GetDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute?.Description;
    }

    private Type? FindEnumType(string key)
    {
        var types = typeSourceProvider.Assemblies.SelectMany(x => x.GetTypes());

        foreach (Type type in types)
        {
            if (type.IsEnum)
            {
                if (type.Namespace is not null &&
                    type.Namespace.StartsWith("InternshipBackend.Data") &&
                    type.Name == key)
                {
                    return type;
                }
            }
        }

        return null;
    }
}
