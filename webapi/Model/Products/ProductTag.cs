using System.ComponentModel;
using System.Text.Json.Serialization;

namespace webapi.Model.Products
{
    public class ProductTag
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }

        [ReadOnly(true)]
        [JsonIgnore]
        public virtual Product Product { get; set; }

        [ReadOnly(true)]
        public virtual Tag Tag { get; set; }
    }
}