using NLayerEmarket.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Application.Collections.Basket
{
    public interface IBasketCollection
    {

        BasketModel Create(BasketModel basket);


        List<BasketModel> Get();


        BasketModel Get(string ID);



        void Update(string id, BasketModel basket);

    }
}
