using CheckoutApp.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutApp.Repositories
{
    public interface IBasketRepository
    {
        public Task<long> CreateBasket(Basket basket);
        public void UpdateBasket(Basket basket);
        public Basket GetBasket(long id);
    }
}
