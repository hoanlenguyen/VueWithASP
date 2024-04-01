using System.ComponentModel.DataAnnotations.Schema;
using webapi.Model.BaseEntity;

namespace webapi.Model.Production
{
    public class ProductTag: Entity
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
