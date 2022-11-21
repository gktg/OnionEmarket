using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Application.Collections.Basket;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Collections;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.ViewModels;
using NLayerEmarket.Insfastructure.Tools;

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
