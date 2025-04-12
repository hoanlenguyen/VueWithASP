using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Products
{
    public class Product : BaseAuditModel, IAuditModel
    {
        [Required]
        [MaxLength(LimitLength.FullName)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(LimitLength.FullDescription)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(LimitLength.ShortDescription)]
        public string ShortDescription { get; set; } = string.Empty;

        [MaxLength(LimitLength.ShortUrl)]
        public string? AvatarUrl { get; set; } = string.Empty;

        [Precision(8, 2)]
        [Range(0, 999999, ErrorMessage = "Value should be greater than or equal to {0}")]
        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual ProductCategory? Category { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand? Brand { get; set; }


        public virtual ICollection<ProductTag> ProductTags { get; set; } = [];

    }
}