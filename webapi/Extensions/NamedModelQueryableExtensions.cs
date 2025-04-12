using Microsoft.EntityFrameworkCore;
using webapi.Model.BaseEntities;

namespace webapi.Extensions;
public static class NamedModelQueryableExtensions
{
    public static IQueryable<T> Include<T>(IQueryable<T> queryable, List<string> includes) where T : class, INamedModel
    {
        foreach (string include in includes.Where(i => !string.IsNullOrWhiteSpace(i)))
        {
            queryable = queryable.Include(include);
        }

        return queryable;
    }

    public static IQueryable<T> SearchBy<T>(IQueryable<T> queryable, string searchKey) where T : INamedModel
    {
        if (!string.IsNullOrWhiteSpace(searchKey))
        {
            queryable = queryable.Where((T x) => EF.Functions.Collate(x.Name, "SQL_Latin1_General_CP1_CI_AS").Contains(searchKey));
        }

        return queryable;
    }

    public static IQueryable<T> FilterEnabled<T>(IQueryable<T> queryable) where T : INamedEnabledStateModel
    {
        return queryable.Where(d => d.IsEnabled);
    }

    public static IQueryable<T> SortBySortOrder<T>(IQueryable<T> queryable) where T : INamedSortableModel
    {
        return queryable.OrderBy(x => x.SortOrder).ThenBy(x => x.Name);
    }

    public static IQueryable<T> SortByName<T>(IQueryable<T> queryable) where T : INamedModel
    {
        return queryable.OrderBy(x => x.Name);
    }
}
