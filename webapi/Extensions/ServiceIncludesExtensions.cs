using webapi.Data;
using webapi.Model.BaseEntities;

namespace webapi.Extensions;

public static class ServiceIncludesExtensions
{
    public static IQueryable<object> WithLookupIncludes<T>(this StoreDbContext schroleDbContext, IQueryable<T> source) where T : class, IModel
    {
        IQueryable<object> result;

        if (schroleDbContext.Extensions.TryGetExtension<IQueryableVisitor<IQueryableVisitor<IModel>, IModel>>(out var queryableVisitor) && queryableVisitor.CanVisit(typeof(T)))
        {
            result = queryableVisitor.WithLookupIncludes(source.AsQueryable());
        }
        else
        {
            result = source;
        }

        return result;
    }
}