using System.ComponentModel.DataAnnotations;

namespace webapi.Model.BaseEntity
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }

    public interface IAuditEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseAuditEntity : BaseEntity, IAuditEntity
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(1);
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
