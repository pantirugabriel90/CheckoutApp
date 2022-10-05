using CheckoutApp.Models.Queries;
using CheckoutApp.Repositories;
using System.Collections.Generic;

namespace CheckoutApp.Services
{
    public class BasketQueryHandler : IBasketQueryHandler { 

        private IBasketRepository BasketRepository { get; set; }
        public BasketQueryHandler(IBasketRepository basketRepository)
        {
            BasketRepository = basketRepository;
        }
        public BasketQueryResult ExecuteBasketQuery(long id)
        {
            var basket = BasketRepository.GetBasket(id);
            if (basket == null) return null;
            var result = new BasketQueryResult()
            {
                Customer = basket.Customer,
                PaysVAT = basket.PaysVAT,
                TotalGross = basket.TotalGross,
                Status = basket.Status,
                TotalNet = basket.TotalNet,
                Articles = new List<Article>()
            };
            if (basket.Articles != null) {
                foreach (var article in basket.Articles)
                {
                    result.Articles.Add(new Article { Name = article.Name, Price = article.Price });
                }
            }
            return result;
        }
    }
}
