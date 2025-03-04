namespace webapi.Model.BaseEntities
{
    public interface IAuditEntity : IBaseEntity
    {
        public string? ChangedByUser { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public abstract class BaseAuditEntity : BaseEntity, IAuditEntity
    {
        //[SystemValue]
        public virtual string? ChangedByUser { set; get; }

        //[SystemValue]
        public byte[] RowVersion { set; get; } = new byte[0];
    }
}