namespace webapi.Model.BaseEntities;

public interface IAuditModel : IIdentityModel
{
    public string? ChangedByUser { get; set; }
    public byte[] RowVersion { get; set; }
}

public abstract class BaseAuditModel : BaseIdentityModel<BaseAuditModel>, IAuditModel
{
    //[SystemValue]
    public virtual string? ChangedByUser { set; get; }

    //[SystemValue]
    public byte[] RowVersion { set; get; } = new byte[0];
}