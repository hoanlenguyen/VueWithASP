using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace webapi.Model.Identity
{
    public class UserToken : IdentityUserToken<int>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}