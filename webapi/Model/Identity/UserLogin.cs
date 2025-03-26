using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace webapi.Model.Identity
{
    public class UserLogin : IdentityUserLogin<int>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}