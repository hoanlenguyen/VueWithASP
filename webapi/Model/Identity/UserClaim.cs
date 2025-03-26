using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace webapi.Model.Identity
{
    public class UserClaim : IdentityUserClaim<int>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}