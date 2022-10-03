using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Domain.Enums;
using NLayerEmarket.Domain.Entities;
using System.Security.Claims;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.ViewModels;
using AutoMapper;

namespace NLayerEmarket.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IMapper _mapper;


        public CategoriesController(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _mapper = mapper;
        }

        [Route("/categories/GetAllCategories/")]
        [HttpGet]
        public List<CategoryVM> GetCategories()
        {
            List<Category> categoriesList = _categoryReadRepository.GetAll().ToList();
            List<CategoryVM> categories = _mapper.Map<List<CategoryVM>>(categoriesList);
            return categories;
        }
    }
}
