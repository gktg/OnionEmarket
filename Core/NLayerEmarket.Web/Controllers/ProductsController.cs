using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Entities;

namespace NLayerEmarket.Web.Controllers
{


    public class ProductsController : Controller
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public void Get()
        {
            List<Product> productList2 = _productReadRepository.GetAll().ToList();
        }
    }
}
