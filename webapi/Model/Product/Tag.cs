using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Product
{
    public class Tag : BaseAuditEntity, IAuditEntity
    {
        [Required]
        [MaxLength(LimitLength.ShortName)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ProductTag> ProductTags { get; set; } = [];
    }
}