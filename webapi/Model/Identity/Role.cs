using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class Role : IdentityRole<int>, IAuditEntity
    {
        [MaxLength(100)]
        public override string Name { get; set; }

        [MaxLength(100)]
        public override string NormalizedName { get; set; }

        [MaxLength(50)]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public virtual string? ChangedByUser { set; get; }

        public byte[] RowVersion { set; get; } = new byte[0];

        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual IList<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();
    }
}