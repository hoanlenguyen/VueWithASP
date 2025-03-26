using System.ComponentModel.DataAnnotations;

namespace webapi.Model.Identity
{
    public partial class User
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