using AutoMapper;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;

namespace MegaMart.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>
                ().ForMember(x => x.CategoryName,
                op => op.MapFrom(src => src.Category.Name)).ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
    }
}
