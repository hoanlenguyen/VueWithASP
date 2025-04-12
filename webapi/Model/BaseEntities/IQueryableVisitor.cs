using webapi.Extensions.Models;

namespace webapi.Model.BaseEntities;

public interface IQueryableVisitor<U> : IHostedExtension where U : IModel
{
    Task<U?> FirstOrDefault(IQueryable<U> source, U queryEntity, CancellationToken cancellationToken);

    IQueryable<U> WithLookupIncludes(IQueryable<U> source);
}

public interface IQueryableVisitor<out T, U> : IQueryableVisitor<U> where T : IQueryableVisitor<U> where U : IModel
{
    bool CanVisit(Type typeToVisit);
}
