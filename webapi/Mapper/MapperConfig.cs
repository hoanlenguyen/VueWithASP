using Mapster;
using webapi.Model.Lookup;
using webapi.Model.Product;

namespace webapi.Mapper
{
    public static class MapperConfig
    {
        public static void AddMapperConfigs()
        {
            _ = TypeAdapterConfig<Product, ProductDTO>.NewConfig()
                            .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
                            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty)
                            .Map(dest => dest.Tags, src => src.ProductTags.Select(t => new LookupModel { Id = t.TagId, Name = t.Tag.Name }).ToList())
                            ;

            _ = TypeAdapterConfig<ProductDTO, Product>.NewConfig()
                            .Map(dest => dest.ProductTags, src => src.Tags.Select(t => new ProductTag { TagId = t.Id, ProductId = src.Id }))
                            ;
        }
    }
}