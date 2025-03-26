using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public partial class User : IdentityUser<int>, IAuditEntity, INotifyPropertyChanged
    {
        public UserType UserType { get; set; } = UserType.EndUser;
        public bool IsActive { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public string? ChangedByUser { get; set; }
        public byte[] RowVersion { get; set; } = new byte[0];

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}