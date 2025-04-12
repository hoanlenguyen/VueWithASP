namespace webapi.Model.BaseEntities;

public interface IEnabledStateModel : IModel
{
    public bool IsEnabled { get; set; }
}