using Microsoft.EntityFrameworkCore;
using System.Reflection;
using webapi.Extensions;

namespace webapi.Data.Extensions;
public static class DbContextSetAccessor
{
    private static Type DbContextType { get; } = typeof(StoreDbContext);

    private static MethodInfo? DbSetMethod { get; }

    static DbContextSetAccessor()
    {
        DbSetMethod = DbContextType.GetMethod("Set", 1, Type.EmptyTypes);
    }

    public static IQueryable<object>? GetQueryableFromTypeName(this DbContext context, string typeName)
    {
        var type = NamedModelExtensions.GetModelType(typeName);
        return context.GetQueryableFromType(type);
    }

    //public static IQueryable<object>? GetQueryableFromSearchableTypeName(this DbContext context, string typeName)
    //{
    //    var type = EntityFramework.Models.SearchableExtensions.GetModelType(typeName);
    //    return context.GetQueryableFromType(type);
    //}

    public static IQueryable<object>? GetQueryableFromType(this DbContext context, Type t)
    {
        return DbSetMethod?.MakeGenericMethod(t).Invoke(context, null) as IQueryable<object>;
    }
}
