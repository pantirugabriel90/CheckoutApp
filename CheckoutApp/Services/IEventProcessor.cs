using CheckoutApp.Models;
using System.Threading.Tasks;

namespace CheckoutApp.Services
{
    public interface IEventProcessor
    {
        bool ProcessAddArticle(AddArticleEvent evnt, long basketId);
        bool ProcessUpdateBasket(UpdateBasketEvent evnt, long basketId);
        Task<long?> ProcessCreateBascket(CreateBasketEvent evnt);
    }
}
