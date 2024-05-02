using webapi.Model.DTO;
using webapi.Model.Lookup;

namespace webapi.Model.Product
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; } = string.Empty;

        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int? BrandId { get; set; }
        public string? BrandName { get; set; }

        public virtual IEnumerable<LookupModel> Tags { get; set; } = Enumerable.Empty<LookupModel>();
    }
}