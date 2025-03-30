using System.ComponentModel.DataAnnotations;
using webapi.Model.BaseEntities;

namespace webapi.Model.Identity
{
    public class UserDetail : BaseAuditEntity, IAuditEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = default!;

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
    }
}
