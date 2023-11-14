using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Enum;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class Product : BaseAuditEntity
    {
        [Required]
        [MaxLength(LimitLength.FullName)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(LimitLength.FullDescription)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(LimitLength.ShortDescription)]
        public string ShortDescription { get; set; } = string.Empty;

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual ProductCategory? Category { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
    }
}