using AutoMapper;
using MegaMart.Core.DTO;
using MegaMart.Core.Entities.Product;

namespace MegaMart.API.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
