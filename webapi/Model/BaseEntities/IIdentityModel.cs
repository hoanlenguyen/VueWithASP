namespace webapi.Model.BaseEntities;

public interface IIdentityModel : IModel
{
    int Id { get; set; }
}

public abstract class BaseIdentityModel<T> : IIdentityModel where T : IIdentityModel
{
    public int Id { get; set; }
}