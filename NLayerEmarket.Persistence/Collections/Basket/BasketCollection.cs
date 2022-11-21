using MongoDB.Driver;
using NLayerEmarket.Application.Collections.Basket;
using NLayerEmarket.Domain.Collections;


namespace NLayerEmarket.Persistence.Collections.Basket
{
    public class BasketCollection : IBasketCollection
    {
        private readonly IMongoCollection<BasketModel> _basket;


        public BasketCollection(IMongoClient client)
        {
            _basket = MongoDbManager.GetDatabase(client).GetCollection<BasketModel>("basket");

        }

        public BasketModel Create(BasketModel basket)
        {

            basket.Id = Guid.NewGuid().ToString();
            _basket.InsertOne(basket);

            return basket;
        }

        public List<BasketModel> Get()
        {
            return _basket.Find(basket => true).ToList();
        }

        public BasketModel Get(string ID)
        {
            return _basket.Find(basket => basket.Id == ID).FirstOrDefault();
        }


        public void Update(string id, BasketModel basket)
        {
            _basket.ReplaceOne(bask => bask.Id == id, basket);
        }
    }
}
