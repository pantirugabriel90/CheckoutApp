using CheckoutApp.Models;
using CheckoutApp.Models.Domain;
using CheckoutApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutApp.Services
{
    public class EventProcessor : IEventProcessor
    {

        private IBasketRepository BasketRepository { get; set; }
        public ICustomLogger Logger { get; set; }
        public EventProcessor(IBasketRepository basketRepository, ICustomLogger logger)
        {
            BasketRepository = basketRepository;
            Logger = logger;
        }
        public bool ProcessAddArticle(AddArticleEvent evnt, long basketId)
        {
            var basket = BasketRepository.GetBasket(basketId);
            if (basket != null)
            {
                try
                {
                    AddArticleToBasket(basket, evnt);

                    BasketRepository.UpdateBasket(basket);
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Exception encountered while adding article. {ex.Message.ToString()}");
                    return false;
                }
            }

            return false;
        }
        public void AddArticleToBasket(Basket basket, AddArticleEvent evnt) {
            var article = new Article()
            {
                Name = evnt.Name,
                Price = evnt.Price,
            };
            if (basket.Articles == null) { basket.Articles = new List<Article>(); }

            basket.Articles.Add(article);
            // assumed article price was without tva
            //to get price with tva we need to add the price after tva
            if (basket.PaysVAT)
            {
                basket.TotalGross = basket.TotalGross + GetPricePlusTva(article.Price);
            }
            else {
                basket.TotalGross = basket.TotalGross + article.Price;
            }
            basket.TotalNet = basket.TotalNet + article.Price;
        }
        public double GetPricePlusTva(double price)
        {
            return (price + price * Constants.VTA);
        }
        public async Task<long?> ProcessCreateBascket(CreateBasketEvent evnt)
        {
            try
            {
                var basket = MapCreateBasketEventToBasket(evnt);
                var result = await BasketRepository.CreateBasket(basket);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Exception encountered while creating basket. {ex.Message.ToString()}");
            }
            return null;
        }
        public Basket MapCreateBasketEventToBasket(CreateBasketEvent evnt) {
            var basket = new Basket()
            {
                Customer = evnt.Customer,
                PaysVAT = evnt.PaysVTA,
                Status = Status.Closed,
                TotalGross = 0,
                TotalNet = 0,
            };
            return basket;
        }

        public bool ProcessUpdateBasket(UpdateBasketEvent evnt, long basketId)
        {
            var basket = BasketRepository.GetBasket(basketId);
            if (basket != null)
            {
                try
                {
                    UpdateBasketFromEvent(basket, evnt);
                    BasketRepository.UpdateBasket(basket);
                    return true;
                }
                catch (Exception ex)
                {

                    Logger.LogError($"Exception encountered while updating basket. {ex.Message.ToString()}");
                    return false;
                }
            }

            return false;
        }

        public void UpdateBasketFromEvent(Basket basket, UpdateBasketEvent evnt) {

            basket.Status = evnt.Status;
        }
    }
}
