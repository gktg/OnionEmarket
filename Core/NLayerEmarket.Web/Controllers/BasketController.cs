using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerEmarket.Application.Collections.Basket;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Collections;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.ViewModels;
using NLayerEmarket.Insfastructure.Tools;

namespace NLayerEmarket.Web.Controllers
{
    public class BasketController : Controller
    {

        private readonly IProductReadRepository _productReadRepository;
        private readonly IBasketCollection _basketCollection;
        private readonly IMapper _mapper;


        public BasketController(IProductReadRepository productReadRepository, IBasketCollection basketCollection, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _basketCollection = basketCollection;
            _mapper = mapper;
        }


        [Route("/Basket/AddProductToBasket/{productID}")]
        public List<ProductVM> AddProductToBasket(string productID)
        {
            Product product = _productReadRepository.GetByIdAsync(productID).Result;

            List<ProductVM> oldBasket = HttpContext.Session.GetObject<List<ProductVM>>("Basket");

            List<ProductVM> newBasket = new List<ProductVM>();


            var productModel = new ProductVM()
            {
                ID = product.ID.ToString(),
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
                    if (item.ID == productID)
                    {
                        item.Quantity++;
                        float productPrice = product.Price;
                        float basketPrice = item.Quantity * productPrice;
                        item.Price = basketPrice;
                        ProductVM? x = newBasket.FirstOrDefault(x => x.ID == productID);
                        newBasket.Remove(x);
                    }

                    newBasket.Add(item);


                    BasketModel? basket = _basketCollection.Get().Where(x => x.userId == HttpContext.Session.GetString("ID")).LastOrDefault();


                    BasketModel basketCollection = new BasketModel();

                    basketCollection.ProductList = newBasket;
                    basketCollection.userId = HttpContext.Session.GetString("ID");
                    basketCollection.Id = basket.Id;

                    _basketCollection.Update(basket.Id, basketCollection);

                }
            }
            else
            {
                BasketModel basketCollection = new BasketModel();


                basketCollection.ProductList = newBasket;
                basketCollection.userId = HttpContext.Session.GetString("ID");

                _basketCollection.Create(basketCollection);
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

        [Route("/Basket/DeleteProductFromBasket/{productID}")]
        public List<ProductVM> DeleteProductFromBasket(string productID)
        {



            List<ProductVM> basketList = HttpContext.Session.GetObject<List<ProductVM>>("Basket");

            ProductVM deleted = basketList.FirstOrDefault(x => x.ID == productID);

            if (deleted.Quantity > 1)
            {
                var productOnePrice = deleted.Price / deleted.Quantity;
                deleted.Quantity--;
                deleted.Price = (int)(productOnePrice * deleted.Price);
                HttpContext.Session.SetObject("Basket", basketList);

            }
            else
            {
                basketList.Remove(deleted);
                HttpContext.Session.SetObject("Basket", basketList);
            }

            BasketModel? basket = _basketCollection.Get().Where(x => x.userId == HttpContext.Session.GetString("ID")).LastOrDefault();

            BasketModel basketCollection = new BasketModel();

            basketCollection.ProductList = basketList;
            basketCollection.userId = HttpContext.Session.GetString("ID");
            basketCollection.Id = basket.Id;

            _basketCollection.Update(basket.Id, basketCollection);

            return GetBasket();
        }
    }
}
