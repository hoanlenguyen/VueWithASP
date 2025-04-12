namespace webapi.Model.BaseEntities;

public interface IIdentityModel : IModel
{
    int Id { get; set; }
}

public abstract class BaseIdentityModel : IIdentityModel
{
    public int Id { get; set; }
}