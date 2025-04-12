using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace webapi.Serialization;
public class NamedModelContractResolver : CamelCasePropertyNamesContractResolver
{
    private readonly Type _targetType;

    public NamedModelContractResolver(Type targetType) => _targetType = targetType;

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        => base.CreateProperties
        (
            _targetType.IsAssignableFrom(type) ? _targetType : type,
            memberSerialization
        );
}