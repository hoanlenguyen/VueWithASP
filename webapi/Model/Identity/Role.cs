using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class Role : IdentityRole<int>, IAuditEntity
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(1);
        public int? CreatorUserId { get; set; }

        [MaxLength(100)]
        public override string Name { get; set; }

        [MaxLength(100)]
        public override string NormalizedName { get; set; }

        [MaxLength(50)]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual IList<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
    }
}