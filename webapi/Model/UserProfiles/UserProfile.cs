using System.ComponentModel.DataAnnotations;
using webapi.Model.Identity;

namespace webapi.Model.UserProfiles
{
    public class UserProfile : User
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string DisplayName { get; set; } = string.Empty;
    }
}