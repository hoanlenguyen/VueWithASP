namespace webapi.Model.BaseEntities
{
    public interface IAuditEntity : IBaseEntity
    {
        public string? ChangedByUser { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public abstract class BaseAuditEntity : BaseEntity, IAuditEntity
    {
        //private string? _changedByUser;
        //[SystemValue]
        public virtual string? ChangedByUser { set; get; }

        //private byte[] _rowVersion = new byte[0];

        //[SystemValue]
        public byte[] RowVersion { set; get; } = new byte[0];
    }
}