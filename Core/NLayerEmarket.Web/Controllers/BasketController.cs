using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.ViewModels;
using NLayerEmarket.Insfastructure.Tools;
using NLayerEmarket.Web.Tools;

namespace NLayerEmarket.Web.Controllers
{
    public class BasketController : Controller
    {

        private readonly IProductReadRepository _productReadRepository;

        public BasketController(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }


        [Route("/Basket/AddProductToBasket/{productID}")]
        public List<ProductVM> AddProductToBasket(string productID)
        {
            Product product = _productReadRepository.GetByIdAsync(productID).Result;
                
            List<ProductVM> oldBasket = HttpContext.Session.GetObject<List<ProductVM>>("Basket");

            List<ProductVM> newBasket = new List<ProductVM>();


            var productModel = new ProductVM()
            {
                ID = product.ID,
                CategoryID = product.Category.ID.ToString(),
                CategoryName = product.Category.Name,
                Name = product.Name,
                Price = product.Price,
                Media = product.Media,
                Stock = product.Stock,
                Quantity = 1,
            };

            newBasket.Add(productModel);

            if (oldBasket != null)
            {
                foreach (var item in oldBasket)
                {
                    if (item.ID == Guid.Parse(productID))
                    {
                        item.Quantity++;
                        float productPrice = product.Price;
                        float basketPrice = item.Quantity * productPrice;
                        item.Price = basketPrice;
                        ProductVM? x = newBasket.FirstOrDefault(x => x.ID == Guid.Parse(productID));
                        newBasket.Remove(x);
                    }

                    newBasket.Add(item);


                }
            }


            HttpContext.Session.SetObject("Basket", newBasket);

            return GetBasket();

        }

        [Route("/Basket/GetBasket/")]
        public List<ProductVM> GetBasket()
        {
            List<ProductVM> basket = HttpContext.Session.GetObject<List<ProductVM>>("Basket");
            return basket;

        }
    }
}
