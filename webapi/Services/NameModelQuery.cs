using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webapi.Data;
using webapi.Data.Extensions;
using webapi.Extensions;
using webapi.Model.BaseEntities;
using webapi.Model.NamedModels;
using webapi.Reflection;
using webapi.Serialization;

namespace webapi.Services
{
    public static class NameModelQuery
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new NamedModelContractResolver(typeof(INamedModel))
        };
        private static readonly Type _queryableINamedEnabledStateModelType = typeof(IQueryable<INamedEnabledStateModel>);
        private static readonly Type _queryableINamedSortableModelType = typeof(IQueryable<INamedSortableModel>);
        private static readonly Type _queryableINamedModelType = typeof(IQueryable<INamedModel>);

        public static async Task<List<INamedModel>?> GetAsync(StoreDbContext dbContext, NamedModelRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TypeName))
            {
                return null;
            }

            try
            {
                var type = NamedModelExtensions.GetModelType(request.TypeName);

                if (type is null)
                {
                    return null;
                }
                var typedQueryable = dbContext.GetQueryableFromTypeName(request.TypeName);
                //var includedQuery = typeof(ServiceIncludesExtensions).InvokeGeneric(nameof(ServiceIncludesExtensions.WithLookupIncludes), type, dbContext, typedQueryable);
                var objectTypedQueryable = typeof(NamedModelQueryableExtensions).InvokeGeneric(nameof(NamedModelQueryableExtensions.SearchBy), type, typedQueryable, request.SearchKey);

                var queryableType = objectTypedQueryable.GetType();
                if (_queryableINamedEnabledStateModelType.IsAssignableFrom(queryableType))
                {
                    objectTypedQueryable = typeof(NamedModelQueryableExtensions).InvokeGeneric(nameof(NamedModelQueryableExtensions.FilterEnabled), type, objectTypedQueryable);
                }

                queryableType = objectTypedQueryable.GetType();
                if (_queryableINamedSortableModelType.IsAssignableFrom(queryableType))
                {
                    objectTypedQueryable = typeof(NamedModelQueryableExtensions).InvokeGeneric(nameof(NamedModelQueryableExtensions.SortBySortOrder), type, objectTypedQueryable);
                }
                else if (_queryableINamedModelType.IsAssignableFrom(queryableType))
                {
                    objectTypedQueryable = typeof(NamedModelQueryableExtensions).InvokeGeneric(nameof(NamedModelQueryableExtensions.SortByName), type, objectTypedQueryable);
                }

                var resultQuerably = objectTypedQueryable as IQueryable<INamedModel>;

                if (resultQuerably == null)
                {
                    return null; // Handle the case where the cast fails
                }

                if (request.Skip.HasValue)
                {
                    resultQuerably = resultQuerably.Skip(request.Skip.Value);
                }

                if (request.Take.HasValue)
                {
                    resultQuerably = resultQuerably.Take(request.Take.Value);
                }

                var result = await resultQuerably.AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {

                return [];
            }

        }
    }
}
