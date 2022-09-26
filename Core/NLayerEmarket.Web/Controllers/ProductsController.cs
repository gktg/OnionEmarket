using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.ViewModels;

namespace NLayerEmarket.Web.Controllers
{


    public class ProductsController : Controller
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public IActionResult Products()
        {
            return View();
        }

        [Route("/products/GetAllProducts/")]
        [HttpGet]
        public List<ProductVM> GetAllProducts()
        {
            
            try
            {
                List<Product> productList = _productReadRepository.GetAll().ToList();
                List<ProductVM> productVMList = _mapper.Map<List<ProductVM>>(productList);
                return productVMList;
            }
            tcatch (Exception)
            {
                return null;
                throw;
            }

        }
        [Route("/products/GetProductByCategory/")]
        [HttpPost]
        public List<ProductVM> GetProductByCategory(List<Guid> filterCategoryList)
        {
            
            try
            {
                List<Product> productList = _productReadRepository.GetAll().ToList();

                List<Product> filterProducts = new List<Product>();


                foreach (var item in filterCategoryList)
                {
                    for (var i = 0; i < productList.Count; i++)
                    {
                        if (productList[i].Category.ID == item)
                        {
                            filterProducts.Add(productList[i]);
                        }
                    }
                }

                List<ProductVM> productVMList = _mapper.Map<List<ProductVM>>(filterProducts);
                return productVMList;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }




    }
}
