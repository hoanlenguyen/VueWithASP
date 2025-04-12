using System.Diagnostics;
using System.Reflection;
using webapi.Attributes;
using webapi.Model.BaseEntities;

namespace webapi.Reflection;

[DebuggerDisplay("{Name} IsIModel:{IsIModel} IsIModelCollection:{IsIModelCollection} IsReadOnlyReference:{IsReadOnlyReference}")]
public class PropertyAccessor
{
    public Type PropertyType { get; }
    public string Name { get; }
    public MethodInfo? GetMethod { get; }
    public MethodInfo? SetMethod { get; }
    public bool IsGetSetDefined { get; }
    public bool IsIModel { get; }
    public bool IsReadOnlyReference { get; }
    public bool IsReadOnlyReferenceMutable { get; }
    public bool IsReadOnlyValue { get; }
    public bool IsSystemValue { get; }
    public bool IsIModelCollection { get; }
    public bool IsReadOnlyCollection { get; }
    public bool IsCollection { get; }
    public bool DeleteOnNull { get; }
    public Type? CollectionType { get; }
    public bool RequiresSanitization { get; }
    public bool IsFlagsEnum { get; }


    public PropertyAccessor(PropertyInfo propertyInfo)
    {
        PropertyType = propertyInfo.PropertyType;

        var customAttributes = propertyInfo.GetCustomAttributes();

        Name = propertyInfo.Name;
        GetMethod = propertyInfo.GetGetMethod();
        SetMethod = propertyInfo.GetSetMethod();

        IsGetSetDefined = GetMethod != null && SetMethod != null;
        IsIModel = IModelType.IsAssignableFrom(PropertyType);
        IsReadOnlyReference = ReadOnlyReferenceType.IsAssignableFrom(PropertyType) || customAttributes.OfType<ReadOnlyReferenceAttribute>().Any();
        //IsReadOnlyReferenceMutable = customAttributes.OfType<MutableReferenceAttribute>().Any();
        //IsReadOnlyValue = customAttributes.OfType<ReadOnlyValueAttribute>().Any();
        //IsSystemValue = customAttributes.OfType<SystemValueAttribute>().Any();
        //DeleteOnNull = customAttributes.OfType<DeleteOnNullAttribute>().Any();
        //IsReadOnlyCollection = customAttributes.OfType<ReadOnlyCollectionAttribute>().Any();
        //RequiresSanitization = customAttributes.OfType<SanitizeInputAttribute>().Any();

        if (PropertyType.IsEnum)
        {
            IsFlagsEnum = PropertyType.GetCustomAttributes<FlagsAttribute>().Any();
        }

        if (PropertyType.IsGenericType)
        {
            IsCollection = ICollectionType == PropertyType.GetGenericTypeDefinition();
            CollectionType = PropertyType.GenericTypeArguments.FirstOrDefault(t => IModelType.IsAssignableFrom(t));
            IsIModelCollection = IsCollection && CollectionType != null;
        }
    }

    public object? Get(object propertyInstance)
    {
        return GetMethod?.Invoke(propertyInstance, null);
    }

    public void Set(object propertyInstance, object? value)
    {
        SetMethod?.Invoke(propertyInstance, new object?[] { value });
    }

    public static Type ReadOnlyReferenceType = typeof(IReadOnlyReference);
    public static Type IModelType = typeof(IModel);
    public static Type ICollectionType = typeof(ICollection<>);
}

