using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class User : IdentityUser<int>, IAuditEntity
    {
        //[Required]
        //[MaxLength(100)]
        //public override string UserName { get; set; }

        //[MaxLength(100)]
        //public override string? NormalizedUserName { get; set; }

        //[MaxLength(100)]
        //[ProtectedPersonalData]
        //public virtual string? Email { get; set; }

        //[MaxLength(100)]
        //public virtual string? NormalizedEmail { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public UserType UserType { get; set; } = UserType.EndUser;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(1);
        public int? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}