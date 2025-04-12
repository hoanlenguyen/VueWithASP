using Microsoft.EntityFrameworkCore.ChangeTracking;
using webapi.Extensions.Models;
using webapi.Model.BaseEntities;

namespace webapi.Plugins.Observers
{
    public interface IModelObserver : IHostedExtension
    {
        void ObserveModification(IModel model, EntityEntry entityEntry);
        Task ActionModifications(int? changedByUserId, CancellationToken cancellationToken = default);
    }

    public interface IPluginModelObserver : IModelObserver
    {
        IEnumerable<IModelObserver> GetObservers(Type typeToObserve);
        bool HasObserver(Type typeToObserve);
    }
}
