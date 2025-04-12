namespace webapi.Model.BaseEntities;

public interface IAuditNamedSortableEnabledStateModel : IAuditModel, INamedModel, INamedEnabledStateModel, INamedSortableModel
{
}

public abstract class AuditNamedSortableEnabledStateModel<T> : BaseAuditModel, INamedSortableEnabledStateModel where T : IAuditModel, INamedModel
{
    public bool IsEnabled { get; set; }
    public virtual string Name { get; set; } = default!;
    public int SortOrder { get; set; }
}