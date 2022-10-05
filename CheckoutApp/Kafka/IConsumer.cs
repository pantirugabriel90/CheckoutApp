using CheckoutApp.Models;

namespace CheckoutApp.Kafka
{
    public interface IConsumer
    {
        void SubscribeToChanges();

        void DelegateAddArticleEvent(string evnt);
        void DelegateCreateBasketEvent(string evnt);
        void DelegateUpdateBasketEvent(string evnt);
    }
}
