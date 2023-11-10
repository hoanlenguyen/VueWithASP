using webapi.Enum;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class ProductCategory : BaseAuditEntity
    {
        [Required]
        [MaxLength(MaxLengthLimit.Name)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxLengthLimit.ShortDescription)]
        public string Description { get; set; } = string.Empty;
    }
}