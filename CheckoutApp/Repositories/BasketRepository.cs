using CheckoutApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutApp.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private BasketContext BasketContext { get; set;  }
        public BasketRepository(BasketContext basketContext)
        {
            BasketContext = basketContext;
        }
        async Task<long> IBasketRepository.CreateBasket(Basket basket)
        {
            var result = await BasketContext.Baskets.AddAsync(basket);
            BasketContext.SaveChanges();
            return result.Entity.BasketID;
        }


        Basket IBasketRepository.GetBasket(long id)
        {
           return  BasketContext.Baskets.Include(x=>x.Articles).FirstOrDefault(b => b.BasketID == id);
        }

        void IBasketRepository.UpdateBasket(Basket basket)
        {
            BasketContext.Baskets.Update(basket);
            BasketContext.SaveChanges();

        }
    }
}
