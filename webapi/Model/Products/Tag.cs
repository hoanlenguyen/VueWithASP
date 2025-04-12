using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntities;

namespace webapi.Model.Products
{
    public class Tag : BaseIdentityModel
    {
        [Required]
        [MaxLength(LimitLength.ShortName)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ProductTag> ProductTags { get; set; } = [];
    }
}