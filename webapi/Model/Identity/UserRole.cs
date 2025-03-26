using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Model.Identity
{
    public class UserRole : IdentityUserRole<int>, INotifyPropertyChanged
    {
        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}