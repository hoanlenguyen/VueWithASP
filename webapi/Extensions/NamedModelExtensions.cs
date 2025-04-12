using System.Collections.Concurrent;
using webapi.Model.BaseEntities;
using webapi.Model.Lookup;

namespace webapi.Extensions;
public static class NamedModelExtensions
{
    static NamedModelExtensions()
    {
        TypeDictionary = new ConcurrentDictionary<string, Type>();

        var assemblyTypes = typeof(NamedModel<>).Assembly.GetTypes();
        var namedModelType = typeof(INamedModel);
        var lookupModelType = typeof(ILookupModel);

        foreach (var type in assemblyTypes)
        {
            if (lookupModelType.IsAssignableFrom(type) && namedModelType.IsAssignableFrom(type))
            {
                TypeDictionary.TryAdd(type.Name, type);
            }
        }
    }

    public static ConcurrentDictionary<string, Type> TypeDictionary { get; }

    public static Type GetModelType(string type)
    {
        return TypeDictionary[type];
    }
}
