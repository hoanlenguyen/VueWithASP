namespace webapi.Paging
{
    public class ProductFilter : BaseFilterDto
    {
        public string? Name { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}