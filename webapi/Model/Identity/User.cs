using Microsoft.AspNetCore.Identity;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class User : IdentityUser<int>, IAuditEntity
    {
        public UserType UserType { get; set; } = UserType.None;

        public string? ChangedByUser { set; get; }

        public byte[] RowVersion { set; get; } = new byte[0];

        public UserDetail? UserDetail { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}