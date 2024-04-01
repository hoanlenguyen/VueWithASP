using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class Brand : BaseAuditEntity
    {
        [Required]
        [MaxLength(LimitLength.FullName)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(LimitLength.ShortDescription)]
        public string Description { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}