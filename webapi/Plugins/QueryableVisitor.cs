using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using webapi.Model.BaseEntities;

namespace webapi.Plugins;

public class QueryableVisitor<T, U> : AbstractPluginProvider<T, QueryableVisitorAttribute>, IQueryableVisitor<T, U> where T : IQueryableVisitor<U> where U : class, IModel
{
    private static readonly ConcurrentDictionary<Type, List<Type>> _typeVisitorTypes = new ConcurrentDictionary<Type, List<Type>>();
    private readonly ConcurrentDictionary<Type, List<T>> _typeVisitors = new ConcurrentDictionary<Type, List<T>>();

    public QueryableVisitor(ILogger<QueryableVisitor<T, U>> logger, IServiceProvider serviceProvider, IOptions<PluginProviderSettings> pluginProviderSettings)
        : base(logger, serviceProvider, pluginProviderSettings)
    {

    }

    public async Task<U?> FirstOrDefault(IQueryable<U> source, U entity, CancellationToken cancellationToken)
    {
        var result = default(U?);

        if (source != null)
        {
            var entityType = entity.GetType();
            foreach (var visitor in GetVisitors(entityType))
            {
                if (result == default(U?))
                {
                    result = await visitor.FirstOrDefault(source, entity, cancellationToken);
                }
            }
        }

        return await Task.FromResult(result);
    }

    public IQueryable<U> WithLookupIncludes(IQueryable<U> source)
    {
        var typeToVisit = source.GetType().GetGenericArguments().First();

        foreach (var visitor in GetVisitors(typeToVisit))
        {
            source = visitor.WithLookupIncludes(source);
        }

        return source;
    }

    public bool CanVisit(Type typeToVisit)
    {
        return GetVisitors(typeToVisit).Any();
    }

    private IEnumerable<T> GetVisitors(Type typeToVisit)
    {
        return _typeVisitors.GetOrAdd(typeToVisit, type =>
        {
            if (_typeVisitorTypes.TryGetValue(type, out var visitorTypes))
            {
                List<T> visitors = new List<T>();

                foreach (Type visitorType in visitorTypes)
                {
                    try
                    {
                        var visitor = (T?)_serviceProvider.GetService(visitorType);

                        if (visitor != null)
                        {
                            visitors.Add(visitor);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to Get Visitor");
                    }
                }
                return visitors;
            }
            return new List<T>();
        });
    }

    protected override void AddPlugin(Type visitor, QueryableVisitorAttribute pluginInstanceAttribute)
    {
        if (PluginType.IsAssignableFrom(pluginInstanceAttribute.Visitor))
        {
            _typeVisitorTypes.AddOrUpdate(pluginInstanceAttribute.Visited,
                visited =>
                {
                    return new List<Type> { visitor };
                },
                (visited, visitors) =>
                {
                    if (!visitors.Contains(visitor))
                    {
                        visitors.Add(visitor);
                    }
                    return visitors;
                });
        }
    }
}