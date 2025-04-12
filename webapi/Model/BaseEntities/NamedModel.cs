namespace webapi.Model.BaseEntities;

public interface INamedModel : IIdentityModel
{
    string Name { get; set; }
}
