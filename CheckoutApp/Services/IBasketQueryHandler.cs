using CheckoutApp.Models.Queries;

namespace CheckoutApp.Services
{
    public interface IBasketQueryHandler
    {
        public BasketQueryResult ExecuteBasketQuery(long id);
    }
}
