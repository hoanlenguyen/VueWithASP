using webapi.Enum;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class Product : BaseAuditEntity
    {
        [Required]
        [MaxLength(MaxLengthLimit.Name)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxLengthLimit.Description)]
        public string Description { get; set; } = string.Empty;
    }
}
