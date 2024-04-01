using System.ComponentModel.DataAnnotations;
using webapi.Enum;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class Tag: Entity
    {
        [Required]
        [MaxLength(LimitLength.ShortName)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
