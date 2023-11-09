using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Model.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}