namespace webapi.Model.BaseEntities;

public interface ISortable
{
    public int SortOrder { get; set; }
}

public interface INamedSortableModel : INamedModel, ISortable
{
}