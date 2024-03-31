namespace InternshipBackend.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterForInterfacesAndSelf(this IServiceCollection services,ServiceLifetime lifetime, IEnumerable<Type> types, IEnumerable<Type> excludeTypes)
    {
        foreach (var serviceType in types)
        {
            var interfaceTypes = serviceType.GetInterfaces().Where(x => !excludeTypes.Contains(x));
            foreach (var interfaceType in interfaceTypes)
            {
                services.Add(new ServiceDescriptor(interfaceType, serviceType, lifetime));
            }
            
            services.Add(new ServiceDescriptor(serviceType, serviceType, lifetime));
        }

        return services;
    }
}