using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using webapi.Model.BaseEntities;

namespace webapi.Plugins.Observers
{
    public class ModelObserver : AbstractPluginProvider<IModelObserver, ModelObserverAttribute>, IPluginModelObserver
    {
        private static readonly ConcurrentDictionary<Type, List<Type>> _typeObserverTypes = new ConcurrentDictionary<Type, List<Type>>();
        private readonly ConcurrentDictionary<Type, List<IModelObserver>> _typeObservers = new ConcurrentDictionary<Type, List<IModelObserver>>();
        private readonly ConcurrentDictionary<Type, IModelObserver> _observers = new ConcurrentDictionary<Type, IModelObserver>();

        public ModelObserver(ILogger<ModelObserver> logger, IServiceProvider serviceProvider, IOptions<PluginProviderSettings> pluginProviderSettings)
            : base(logger, serviceProvider, pluginProviderSettings)
        {

        }

        public void ObserveModification(IModel entity, EntityEntry entityEntry)
        {
            var entityType = entity.GetType();
            foreach (var observer in GetObservers(entityType))
            {
                observer.ObserveModification(entity, entityEntry);
            }
        }

        public async Task ActionModifications(int? changedByUserId, CancellationToken cancellationToken = default)
        {
            foreach (var observer in _observers.Values)
            {
                await observer.ActionModifications(changedByUserId, cancellationToken);
            }
        }

        public bool HasObserver(Type typeToObserve)
        {
            return GetObservers(typeToObserve).Any();
        }

        public IEnumerable<IModelObserver> GetObservers(Type typeToObserve)
        {
            //for the lifetime of the provider we return the same instance of each observer
            return _typeObservers.GetOrAdd(typeToObserve, type =>
            {
                if (_typeObserverTypes.TryGetValue(type, out var observerTypes))
                {
                    List<IModelObserver> observers = new List<IModelObserver>();

                    foreach (Type observerType in observerTypes)
                    {
                        try
                        {
                            var observer = (IModelObserver?)_serviceProvider.GetService(observerType);

                            if (observer != null)
                            {
                                _observers.TryAdd(observerType, observer);
                                observers.Add(observer);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to Get Observer");
                        }
                    }
                    return observers;
                }
                return new List<IModelObserver>();
            });
        }

        protected override void AddPlugin(Type observer, ModelObserverAttribute pluginInstanceAttribute)
        {
            if (PluginType.IsAssignableFrom(typeof(IModelObserver)))
            {
                _typeObserverTypes.AddOrUpdate(pluginInstanceAttribute.Observed,
                    visited =>
                    {
                        return new List<Type> { observer };
                    },
                    (visited, observers) =>
                    {
                        if (!observers.Contains(observer))
                        {
                            observers.Add(observer);
                        }
                        return observers;
                    });
            }
        }
    }
}
