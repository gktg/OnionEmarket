using AutoMapper;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.ViewModels;

namespace NLayerEmarket.Web.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();

        }
    }


}
