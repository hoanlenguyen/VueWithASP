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
                            .Map(dest => dest.Tags, src => src.ProductTags.Any() ? src.ProductTags.Where(pt => pt.Tag != null).Select(pt => new LookupModel { Id = pt.Tag.Id, Name = pt.Tag.Name }).ToList() : new List<LookupModel>())
                            ;
        }
    }
}