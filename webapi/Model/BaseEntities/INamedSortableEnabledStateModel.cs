namespace webapi.Model.BaseEntities;

public interface INamedSortableEnabledStateModel : INamedModel, INamedEnabledStateModel, INamedSortableModel
{
}

public abstract class NamedSortableEnabledStateModel<T> : BaseIdentityModel, INamedSortableEnabledStateModel where T : INamedSortableEnabledStateModel
{
    public bool IsEnabled { get; set; }
    public virtual string Name { get; set; } = default!;
    public int SortOrder { get; set; }
}